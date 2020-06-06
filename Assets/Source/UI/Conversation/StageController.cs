using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//Stage Controller controls the dialogue proguession and visual, including showing characters, transitioning scenes and playing animations
public class StageController : MonoBehaviour, IPointerClickHandler
{
    public GameObject overlay;
    public GameObject background;
    public GameObject description;
    public GameObject actorPrefab;

    public GameObject actorsStage;

    public ChoiceController choiceController;
    public ResponseController responseController;

    public bool onActing;
    public List<ScriptLine> script;

    private List<OfficerController> officers;
    private List<AnimationController> actors = new List<AnimationController>();

    public void Start() {
        choiceController.gameObject.SetActive(false);
        responseController.gameObject.SetActive(false);
        description.gameObject.SetActive(false);
    }

    public void StartUpStageForScript(List<ScriptLine> scriptToSet, List<OfficerController> actorsOfStage) {
        script = scriptToSet;
        officers = actorsOfStage;

        officers.ForEach(controller => {

            AnimationController actorGameObject = Instantiate(actorPrefab, actorsStage.transform).GetComponent<AnimationController>();

            actorGameObject.GetComponent<Image>().sprite = controller.officerSprite.First();
            actorGameObject.officerId = controller.baseOfficer.id;

            actors.Add(actorGameObject);
        });

        onActing = true;
        StartCoroutine(PlayLine(script[0]));
    }

    private void CleanStageForNextLine(int currentLineIndex) {
        if (currentLineIndex == 0) { return; }

        ScriptLine currentLine = script[currentLineIndex];
        ScriptLine previousLine = script[currentLineIndex - 1];

        if (previousLine.type == LineType.DESCRIPTION && currentLine.type != LineType.DESCRIPTION) {
            responseController.ShowResponse(previousLine.dialogue);

            description.gameObject.SetActive(false);
        }

        if (previousLine.type == LineType.PLAYER_CHOICE) {
            choiceController.gameObject.SetActive(false);
        }
    }

    private IEnumerator PlayLine(ScriptLine line, int index = 0) {
        CleanStageForNextLine(index);

        switch (line.type) {
            case LineType.DESCRIPTION:
                description.gameObject.SetActive(true);
                description.GetComponentInChildren<Text>().text = line.dialogue;
                break;
            case LineType.PLAYER_CHOICE:
                choiceController.ShowChoices(line.playerChoices);
                break;
            case LineType.DEFAULT_LINE:
                responseController.ShowResponse(line.dialogue);
                break;
            case LineType.ANIMATED_LINE:
                AnimationController actorToAnimate = actors.Find(controller => controller.officerId == line.speaker.baseOfficer.id);

                actorToAnimate.Animate(line.animation);
                break;
            default:
                Debug.LogError("Unknown line type: " + line.type);
                break;
        }

        yield return new WaitForSeconds(5);

        if (index + 1 < script.Count) {
            yield return PlayLine(script[index + 1], index + 1);
        }

        onActing = false;
        yield return null;
    }

    public void OnPointerClick(PointerEventData eventData) {
        throw new System.NotImplementedException();
    }
}
