using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationType
{
    ENTER_STAGE,
    FADE_IN,
    FADE_OUT,
    MOVE_TO,
    PACE_AROUND,
    JUMP,
    STRUGGLE
}

public struct ScriptAnimation
{
    AnimationType type;
    int[] initialPosition;
    int[] finalPosition;
    bool faceTowardsLeft;
}

public class AnimationController : MonoBehaviour
{
    ScriptAnimation currentAnimation;

    public int officerId;
    public bool onAnimation;

    public void Animate(ScriptAnimation script) {
        StartCoroutine(PlayAnimation(script));
    }

    private IEnumerator PlayAnimation(ScriptAnimation script) {

        yield return null;
    }
}
