using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationType
{
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

    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
