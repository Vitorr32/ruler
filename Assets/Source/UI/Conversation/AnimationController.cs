using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum AnimationType
{
    ENTER_STAGE,
    EXIT_STAGE,
    FADE_IN,
    FADE_OUT,
    MOVE_TO,
    PACE_AROUND,
    JUMP,
    STRUGGLE
}

public class ScriptAnimation
{
    public AnimationType type;
    public int[] initialPosition;
    public int[] finalPosition;
    public bool faceTowardsLeft;
    public int actorID;
}

public class AnimationController : MonoBehaviour
{
    public RectTransform canvasRectTransfrom;
    private RectTransform rectTransform;
    private Image image;

    private ConversationActor actor;
    private bool onFocusedState;

    public bool onAnimation;

    public void Awake() {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();

        ActorsController.OnActorFocusChange += OnChangeOnActorFocus;

        gameObject.SetActive(false);
    }

    public int GetActorID() {
        return actor.associatedOfficer.baseOfficer.id;
    }

    public void PrepareAnimationController(ConversationActor toSetActor) {
        actor = toSetActor;

        onFocusedState = toSetActor.isFocused;
    }

    public void Animate(ScriptAnimation script) {
        onAnimation = true;
        gameObject.SetActive(true);

        StartCoroutine(PlayAnimation(script));
    }

    private void OnChangeOnActorFocus(int refocusActorID) {
        if (!gameObject.activeSelf) { return; }

        if (refocusActorID != actor.associatedOfficer.GetOfficerID()) { return; }

        if (actor.isFocused == onFocusedState) { return; }

        onFocusedState = actor.isFocused;
        FocusCharacter(actor.isFocused);
    }

    private IEnumerator PlayAnimation(ScriptAnimation script) {


        switch (script.type) {
            case AnimationType.ENTER_STAGE:
                PlayerStageState(script.faceTowardsLeft, 1);
                break;
            case AnimationType.EXIT_STAGE:
                PlayerStageState(script.faceTowardsLeft, 1, true);
                break;
            default:
                Debug.LogError("Unknown animation type: " + script.type);
                break;
        }

        yield return null;
    }

    private void PlayerStageState(bool faceTowardsLeft, int offsetFromOtherActors, bool exit = false) {
        if (faceTowardsLeft || exit) {
            transform.localScale = InvertFaceTowardsDirection(transform.localScale);
        }

        float offsetFromBorder = canvasRectTransfrom.rect.width * ActorsController.StageBorderOffset;

        float outsideStage = faceTowardsLeft ? canvasRectTransfrom.rect.width : 0;

        float insideStage = faceTowardsLeft
            ? canvasRectTransfrom.rect.width - ((rectTransform.rect.width / 2) * (1 + offsetFromOtherActors / 10)) - offsetFromBorder
            : (rectTransform.rect.width / 2) * (1 + offsetFromOtherActors / 10) + offsetFromBorder;

        //Only need to set the position of the gameObject if it's not currently on stage, therefore only if it's not in the stage yet
        if (!exit) {
            rectTransform.anchoredPosition = new Vector2(outsideStage, rectTransform.anchoredPosition.y);
        }

        if (exit && !actor.isFocused) {
            LeanTween.scaleY(gameObject, 1, 0.2f);
        }

        Vector2 startPosition = exit ? new Vector2(insideStage, 1) : new Vector2(outsideStage, 0);
        Vector2 endPosition = exit ? new Vector2(outsideStage, 0) : new Vector2(insideStage, 1);

        //Turn the initial X to Final X tween into a vector 2 so we can add the aplha of the gameobject in the same tween logic
        //So while the tween is going between initalX to finalX, it will go from 0 alpha to 1 alpha at the same time
        LeanTween.value(gameObject, startPosition, endPosition, 2)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnUpdate((Vector2 value) => {
                rectTransform.anchoredPosition = new Vector2(value.x, rectTransform.anchoredPosition.y);

                Color color = image.color;
                color.a = value.y;
                image.color = color;

            })
            .setOnComplete(() => onAnimation = false);
    }

    private void FocusCharacter(bool inFocus) {
        LeanTween.color(image.gameObject, inFocus ? Color.white : Color.grey, .5f)
            .setFromColor(image.color)
            .setOnUpdateColor(newColor => image.color = newColor);

        LeanTween.scale(gameObject,
            inFocus
                ? actor.isOnLeftSideOfStage ? Vector3.one : new Vector3(-1, 1, 1)
                : new Vector3(actor.isOnLeftSideOfStage ? 1 : -1, actor.isOnLeftSideOfStage ? 0.8f : -0.8f, 1f), .5f)
            .setOnComplete(() => onAnimation = false); ;
    }

    private Vector3 InvertFaceTowardsDirection(Vector3 localScale) {
        localScale.x = localScale.x == 1 ? -1 : 1;
        return localScale;
    }
}
