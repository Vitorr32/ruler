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

    public ChoiceController choiceController;
    public ResponseController responseController;
    public ActorsController actorsController;

    public bool onActing;
    public List<ScriptLine> script;

    private List<ConversationActor> actors;

    public void Start() {
        choiceController.gameObject.SetActive(false);
        responseController.gameObject.SetActive(false);
        description.gameObject.SetActive(false);
    }

    public void StartUpStageForScript(List<ScriptLine> scriptToSet, List<ConversationActor> actorsOfStage) {
        onActing = true;

        script = scriptToSet;
        actors = actorsOfStage;
        actorsController.StartUpActorsOfStage(actorsOfStage);

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
                OnActorDialogue(line);
                responseController.ShowResponse(line.dialogue);
                break;
            case LineType.ANIMATED_LINE:
                actorsController.ShowAnimation(line.animations);

                do {
                    yield return new WaitForEndOfFrame();
                } while (actorsController.isAnimatingActors);

                break;
            default:
                Debug.LogError("Unknown line type: " + line.type);
                break;
        }

        yield return new WaitForSeconds(1);

        if (index + 1 < script.Count) {
            yield return PlayLine(script[index + 1], index + 1);
        }

        onActing = false;
        yield return null;
    }

    private void OnActorDialogue(ScriptLine line) {
        for (int i = 0; i < actors.Count; i++) {
            actors[i].isFocused = line.speakers.Find(speaker => speaker.GetOfficerID() == actors[i].associatedOfficer.GetOfficerID()) != null;
        }

        actorsController.RefocusActors(actors);
    }

    public void OnPointerClick(PointerEventData eventData) {
        throw new System.NotImplementedException();
    }
}
