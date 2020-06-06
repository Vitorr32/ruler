using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public struct Context
{
}

public enum Actions
{
    TALK_ABOUT,
    INVITE_TO,
    ASK_ABOUT
}
public enum LineType
{
    DESCRIPTION, //Simple text description with no associated speaker
    ANIMATED_LINE, //Line that is a animation or a change in background
    DEFAULT_LINE, //A line of conversation with a associated speaker and no specific animation
    PLAYER_CHOICE // A line where the player will be given a list of choices to be made
}

public struct ScriptLine
{
    public string dialogue;
    public List<Choice> playerChoices;
    public OfficerController speaker;
    public Sprite background;
    public LineType type;
    public ScriptAnimation animation;
}
public class ConversationController : MonoBehaviour, IPointerClickHandler
{
    private StageController stageController;
    private List<OfficerController> officersInConversation;


    List<ScriptLine> history = new List<ScriptLine>();
    List<Choice> speakerChoices;

    private void Awake() {
        PieceController.onPieceCollided += OnPlayerTriggeredConversation;
    }

    // Start is called before the first frame update
    void Start() {
        stageController = GetComponent<StageController>();

        gameObject.SetActive(false);
    }

    void OnDestroy() {
        PieceController.onPieceCollided -= OnPlayerTriggeredConversation;
    }

    // Update is called once per frame
    void Update() {

    }

    private void SetUpConversationBetweenOfficers(OfficerController source, OfficerController target) {
        officersInConversation = new List<OfficerController> { source, target };

        Relationship[] relationships = GetRelationshipBetweenCharacters(source.baseOfficer, target.baseOfficer);

        List<ScriptLine> writtenScript = WriteInitialConversationScript(source, target, relationships);

        stageController.StartUpStageForScript(writtenScript, officersInConversation);
    }

    private List<ScriptLine> WriteInitialConversationScript(OfficerController source, OfficerController target, Relationship[] relationships) {
        List<ScriptLine> scriptLines = new List<ScriptLine>();

        Dialogue chosenIntroduction = DialoguePooler.ObtainDialogueTypeFromSpeaker(DialogueType.INTRODUCTION, target);

        ScriptLine introduction = new ScriptLine();

        introduction.dialogue = "While walking through the **** you meet your friend " + target.baseOfficer.firstName;
        introduction.type = LineType.DESCRIPTION;

        scriptLines.Add(introduction);

        Dialogue targetIntroduction = DialoguePooler.ObtainDialogueTypeFromSpeaker(DialogueType.INTRODUCTION, target);

        scriptLines.AddRange(ConvertDialogueToScriptLine(targetIntroduction, target));

        ScriptLine initialChoices = new ScriptLine();

        initialChoices.playerChoices = new List<Choice>() {
            new Choice(){ line= "Yolo" },
            new Choice(){ line= "Swag" }
        };
        initialChoices.type = LineType.PLAYER_CHOICE;

        scriptLines.Add(initialChoices);

        return scriptLines;
    }

    private Relationship[] GetRelationshipBetweenCharacters(Officer officer1, Officer officer2) {
        Relationship[] relationships = new Relationship[2];

        relationships[0] = officer1.relationships.Find(relationship => relationship.targetID == officer2.id);
        relationships[1] = officer2.relationships.Find(relationship => relationship.targetID == officer1.id);

        return relationships;
    }

    private List<ScriptLine> ConvertDialogueToScriptLine(Dialogue dialogue, OfficerController speaker) {
        List<ScriptLine> linesOfDialogue = new List<ScriptLine>();

        foreach (string text in dialogue.text) {

            ScriptLine textPart = new ScriptLine();
            textPart.dialogue = text;
            textPart.type = LineType.DEFAULT_LINE;
            textPart.speaker = speaker;

            linesOfDialogue.Add(textPart);
        }

        return linesOfDialogue;
    }
    private void OnPlayerTriggeredConversation(OfficerController source, OfficerController target) {
        if (!source.isPlayer || gameObject.activeSelf) {
            return;
        }

        TickController.instance.gameIsPaused = true;

        gameObject.SetActive(true);

        SetUpConversationBetweenOfficers(source, target);
    }

    private void OnEndOfConversation() {
        TickController.instance.gameIsPaused = false;

        gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData) {
        Debug.Log("Yolo");
    }
}
