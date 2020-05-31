using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public struct ScriptLine
{
    public string dialogue;
    public List<Choice> playerChoices;

    public OfficerController speaker;

    public Sprite background;
}
public class ConversationController : MonoBehaviour, IPointerClickHandler
{
    private StageController stageController;

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
        Relationship[] relationships = GetRelationshipBetweenCharacters(source.baseOfficer, target.baseOfficer);

        List<ScriptLine> writtenScript = WriteConversationScript(source, target, relationships);

        stageController.StartUpStageForScript(writtenScript);
    }

    private List<ScriptLine> WriteConversationScript(OfficerController source, OfficerController target, Relationship[] relationships) {
        List<ScriptLine> scriptLines = new List<ScriptLine>();

        ScriptLine introduction = new ScriptLine();

        string introductionText = "While walking through the **** you meet your friend " + target.baseOfficer.firstName;

        introduction.dialogue = introductionText;

        scriptLines.Add(introduction);

        ScriptLine initialReponse = new ScriptLine();

        initialReponse.playerChoices = new List<Choice>() {
            new Choice(){ line= "Yolo" },
            new Choice(){ line= "Swag" }
        };

        scriptLines.Add(initialReponse);

        return scriptLines;
    }

    private Relationship[] GetRelationshipBetweenCharacters(Officer officer1, Officer officer2) {
        Relationship[] relationships = new Relationship[2];

        relationships[0] = officer1.relationships.Find(relationship => relationship.targetID == officer2.id);
        relationships[1] = officer2.relationships.Find(relationship => relationship.targetID == officer1.id);

        return relationships;
    }

    private void OnPlayerTriggeredConversation(OfficerController source, OfficerController target) {
        if (!source.isPlayer || gameObject.activeSelf) {
            return;
        }

        gameObject.SetActive(true);

        SetUpConversationBetweenOfficers(source, target);
    }

    public void OnPointerClick(PointerEventData eventData) {
        Debug.Log("Yolo");
    }
}
