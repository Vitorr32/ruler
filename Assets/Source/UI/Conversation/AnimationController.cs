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
    }

    public void Animate(ScriptAnimation script) {
        onAnimation = true;
        gameObject.SetActive(true);

        StartCoroutine(PlayAnimation(script));
    }

    private void OnChangeOnActorFocus(ConversationActor updatedActor) {
        if (!gameObject.activeSelf) { return; }

        if (updatedActor.associatedOfficer.GetOfficerID() != actor.associatedOfficer.GetOfficerID()) { return; }

        if (updatedActor.isFocused == actor.isFocused) { return; }

        actor = updatedActor;

        FocusCharacter(updatedActor.isFocused);
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

        float outsideStage = faceTowardsLeft
            ? canvasRectTransfrom.rect.width + (rectTransform.rect.width / 2)
            : (rectTransform.rect.width / 2) * -1;

        float insideStage = faceTowardsLeft
            ? canvasRectTransfrom.rect.width - ((rectTransform.rect.width / 2) * (1 + offsetFromOtherActors / 10)) - offsetFromBorder
            : (rectTransform.rect.width / 2) * (1 + offsetFromOtherActors / 10) + offsetFromBorder;

        //Only need to set the position of the gameObject if it's not currently on stage, therefore only if it's not in the stage yet
        if (!exit) {
            rectTransform.anchoredPosition = new Vector2(outsideStage, rectTransform.anchoredPosition.y);
        }

        Vector2 startPosition = exit ? new Vector2(insideStage, 1) : new Vector2(outsideStage, 0);
        Vector2 endPosition = exit ? new Vector2(outsideStage, 0) : new Vector2(insideStage, 1);

        //Turn the initial X to Final X tween into a vector 2 so we can add the aplha of the gameobject in the same tween logic
        //So while the tween is going between initalX to finalX, it will go from 0 alpha to 1 alpha at the same time
        LeanTween.value(gameObject, startPosition, endPosition, 2)
            .setEase(LeanTweenType.easeInOutQuad)
            .setOnUpdate((Vector2 value) => {
                rectTransform.anchoredPosition = new Vector2(value.x, rectTransform.anchoredPosition.y);

                Color color = image.color;
                color.a = value.y;
                image.color = color;

            })
            .setOnComplete(() => onAnimation = false);
    }

    private void FocusCharacter(bool inFocus) {
        Debug.Log("Changing Focus");
        onAnimation = true;

        LeanTween.color(gameObject, inFocus ? Color.white : Color.grey, 2f);
        LeanTween.scale(gameObject, inFocus ? Vector3.one : new Vector3(0.8f, 0.8f, 1f), 2f).setOnComplete(() => onAnimation = false); ;
    }

    private Vector3 InvertFaceTowardsDirection(Vector3 localScale) {
        localScale.x = localScale.x == 1 ? -1 : 1;
        return localScale;
    }
}
