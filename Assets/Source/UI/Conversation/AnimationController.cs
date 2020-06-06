using Boo.Lang;
using System.Collections;
using System.Collections.Generic;
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
    private ScriptAnimation currentAnimation;
    private Image image;

    public int officerId = -1;
    public bool onAnimation;

    public void Start() {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();

        gameObject.SetActive(false);
    }

    public void Animate(ScriptAnimation script) {
        onAnimation = true;
        gameObject.SetActive(true);

        StartCoroutine(PlayAnimation(script));
    }

    private IEnumerator PlayAnimation(ScriptAnimation script) {


        switch (script.type) {
            case AnimationType.ENTER_STAGE:
                PlayEnterStage(script.faceTowardsLeft, 1);
                break;
            default:
                Debug.LogError("Unknown animation type: " + script.type);
                break;
        }

        yield return null;
    }

    private void PlayEnterStage(bool faceTowardsLeft, int offsetFromOtherActors) {
        if (!faceTowardsLeft) {
            transform.localScale = InvertFaceTowardsDirection(transform.localScale);
        }

        float offsetFromBorder = canvasRectTransfrom.rect.width * ActorsController.StageBorderOffset;

        float initialX = faceTowardsLeft
            ? canvasRectTransfrom.rect.width + (rectTransform.rect.width / 2)
            : (rectTransform.rect.width / 2) * -1;

        float finalX = faceTowardsLeft
            ? canvasRectTransfrom.rect.width - ((rectTransform.rect.width / 2) * (1 + offsetFromOtherActors / 10)) - offsetFromBorder
            : (rectTransform.rect.width / 2) * (1 + offsetFromOtherActors / 10) + offsetFromBorder;

        rectTransform.anchoredPosition = new Vector2(initialX, 0f);

        LeanTween.value(initialX, finalX, 2).setOnUpdate((value) => {
            rectTransform.anchoredPosition = new Vector2(value, 0f);

            Color color = image.color;
            color.a = value / (finalX + initialX);
            image.color = color;

        }).setOnComplete(() => onAnimation = false);
    }

    private Vector3 InvertFaceTowardsDirection(Vector3 localScale) {
        localScale.x = -1;
        return localScale;
    }
}
