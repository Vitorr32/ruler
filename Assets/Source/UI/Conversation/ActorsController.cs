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


    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void StartUpActorsOfStage(List<OfficerController> officerOnStage) {
        for (int i = 0; i < officerOnStage.Count; i++) {
            AnimationController animator = animationControllers[i];
            animator.officerId = officerOnStage[i].baseOfficer.id;

            animator.GetComponent<Image>().sprite = officerOnStage[i].officerSprite.First();
        }
    }

    public void ShowAnimation(List<ScriptAnimation> animations) {
        isAnimatingActors = true;

        StartCoroutine(StartAnimations(animations));
    }
    private IEnumerator StartAnimations(List<ScriptAnimation> animations) {
        animations.ForEach(animation => {
            AnimationController actorToAnimate = animationControllers.Find(controller => controller.officerId == animation.actorID);

            actorToAnimate.Animate(animation);
        });

        do {
            yield return new WaitForEndOfFrame();
        } while (!animationControllers.Find(controller => controller.onAnimation));

        isAnimatingActors = false;

        yield return null;
    }
}
