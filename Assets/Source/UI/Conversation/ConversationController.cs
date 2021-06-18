using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ConversationActor
{
    public OfficerController associatedOfficer;
    public bool isOnLeftSideOfStage;
    public bool isFocused;
    public int slot;
}
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
    //Who is talking in the current line (Used to know what character will be focused on the screen)
    public List<OfficerController> speakers;
    public Sprite background;
    public LineType type;
    public List<ScriptAnimation> animations;
}
public class ConversationController : MonoBehaviour
{
    /*
    private StageController stageController;
    private List<ConversationActor> actorsInConversation;


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
        actorsInConversation = new List<ConversationActor>() {
            new ConversationActor(){ associatedOfficer = source, isFocused = true, isOnLeftSideOfStage = true },
            new ConversationActor(){ associatedOfficer = target, isFocused = true, isOnLeftSideOfStage = false }
        };

        RelationshipTemp[] relationships = GetRelationshipBetweenCharacters(source.baseOfficer, target.baseOfficer);

        List<ScriptLine> writtenScript = WriteInitialConversationScript(source, target, relationships);

        stageController.StartUpStageForScript(writtenScript, actorsInConversation);
    }

    private List<ScriptLine> WriteInitialConversationScript(OfficerController source, OfficerController target, RelationshipTemp[] relationships) {
        List<ScriptLine> scriptLines = new List<ScriptLine>();

        ScriptLine playerEnterSage = new ScriptLine();
        playerEnterSage.type = LineType.ANIMATED_LINE;
        playerEnterSage.animations = new List<ScriptAnimation>() { new ScriptAnimation() { type = AnimationType.ENTER_STAGE, faceTowardsLeft = false, actorID = source.baseOfficer.id } };

        scriptLines.Add(playerEnterSage);

        ScriptLine introduction = new ScriptLine();

        introduction.dialogue = "While walking through the **** you meet your friend " + target.baseOfficer.firstName;
        introduction.type = LineType.DESCRIPTION;

        scriptLines.Add(introduction);

        ScriptLine targetEnterStage = new ScriptLine();
        targetEnterStage.type = LineType.ANIMATED_LINE;
        targetEnterStage.animations = new List<ScriptAnimation>() { new ScriptAnimation() { type = AnimationType.ENTER_STAGE, faceTowardsLeft = true, actorID = target.baseOfficer.id } };

        scriptLines.Add(targetEnterStage);

        Dialogue targetIntroduction = DialoguePooler.ObtainDialogueTypeFromSpeaker(DialogueType.INTRODUCTION, target);

        scriptLines.AddRange(ConvertDialogueToScriptLine(targetIntroduction, new List<OfficerController>() { target }));

        Dialogue secondIntroduction = DialoguePooler.ObtainDialogueTypeFromSpeaker(DialogueType.INTRODUCTION, target);

        scriptLines.AddRange(ConvertDialogueToScriptLine(secondIntroduction, new List<OfficerController>() { target }));

        ScriptLine initialChoices = new ScriptLine();

        initialChoices.playerChoices = new List<Choice>() {
            new Choice(){ line= "Yolo" },
            new Choice(){ line= "Swag" }
        };
        initialChoices.type = LineType.PLAYER_CHOICE;

        scriptLines.Add(initialChoices);

        ScriptLine playerExitStage = new ScriptLine();
        playerExitStage.type = LineType.ANIMATED_LINE;
        playerExitStage.animations = new List<ScriptAnimation>() { new ScriptAnimation() { type = AnimationType.EXIT_STAGE, faceTowardsLeft = false, actorID = source.baseOfficer.id } };

        scriptLines.Add(playerExitStage);

        return scriptLines;
    }

    private RelationshipTemp[] GetRelationshipBetweenCharacters(Officer officer1, Officer officer2) {
        RelationshipTemp[] relationships = new RelationshipTemp[2];

        relationships[0] = officer1.relationships.Find(relationship => relationship.targetID == officer2.id);
        relationships[1] = officer2.relationships.Find(relationship => relationship.targetID == officer1.id);

        return relationships;
    }

    private List<ScriptLine> ConvertDialogueToScriptLine(Dialogue dialogue, List<OfficerController> speaker) {
        List<ScriptLine> linesOfDialogue = new List<ScriptLine>();

        foreach (string text in dialogue.text) {

            ScriptLine textPart = new ScriptLine();
            textPart.dialogue = text;
            textPart.type = LineType.DEFAULT_LINE;
            textPart.speakers = speaker;

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
    }
    */
}
