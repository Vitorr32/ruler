using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ActorsController : MonoBehaviour
{
    public static float StageBorderOffset = 0.1f;

    public List<AnimationController> animationControllers = new List<AnimationController>();

    public bool isAnimatingActors;

    public delegate void OnActorReshuffle(Vector3 worldPoint);
    public static event OnActorReshuffle OnActorReshuffleEvent;

    public delegate void OnActorFocus(int updatedActorID);
    public static event OnActorFocus OnActorFocusChange;


    public void StartUpActorsOfStage(List<ConversationActor> actorsOnStage) {
        for (int i = 0; i < actorsOnStage.Count; i++) {
            AnimationController animator = animationControllers[i];

            animator.PrepareAnimationController(actorsOnStage[i]);

            animator.GetComponent<Image>().sprite = actorsOnStage[i].associatedOfficer.officerSprite.First().sprite;
        }
    }
    public void ShowAnimation(List<ScriptAnimation> animations) {
        isAnimatingActors = true;

        StartCoroutine(StartAnimations(animations));
    }

    public IEnumerator RefocusActors(List<ConversationActor> updatedActorsOnStage) {
        isAnimatingActors = true;

        updatedActorsOnStage.ForEach(actor => OnActorFocusChange?.Invoke(actor.associatedOfficer.GetOfficerID()));

        yield return WaitForActorAnimationsToEnd();

        isAnimatingActors = false;

        yield return null;
    }

    private IEnumerator StartAnimations(List<ScriptAnimation> animations) {
        animations.ForEach(animation => {
            AnimationController actorToAnimate = animationControllers.Find(controller => controller.GetActorID() == animation.actorID);

            actorToAnimate.Animate(animation);
        });

        yield return WaitForActorAnimationsToEnd();

        isAnimatingActors = false;

        yield return null;
    }

    private IEnumerator WaitForActorAnimationsToEnd() {
        do {
            yield return new WaitForEndOfFrame();
        } while (animationControllers.Find(controller => controller.onAnimation));
    }
}
