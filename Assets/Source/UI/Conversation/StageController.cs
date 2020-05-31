using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StageController : MonoBehaviour, IPointerClickHandler
{
    public GameObject overlay;
    public GameObject background;

    public ChoiceController choiceController;
    public ResponseController responseController;

    public List<ScriptLine> currentScript;
    public void StartUpStageForScript(List<ScriptLine> script) {
        currentScript = script;

        StartCoroutine(PlayLine(script[0]));
    }

    private IEnumerator PlayLine(ScriptLine line, int index = 0) {

        if (line.playerChoices != null && line.playerChoices.Count != 0) {
            choiceController.ShowChoices(line.playerChoices);
        }

        if (line.dialogue != null && (line.speaker == null || !line.speaker.isPlayer)) {
            Debug.Log("Yolo");
            responseController.ShowResponse(line.dialogue);
        }

        if (index + 1 < currentScript.Count) {
            yield return PlayLine(currentScript[index + 1], index + 1);
        }

        yield return null;
    }

    public void OnPointerClick(PointerEventData eventData) {
        throw new System.NotImplementedException();
    }
}
