using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConditionLine : MonoBehaviour
{
    public delegate void OnConditionLineRemoveClick(GameObject conditionLine, GameObject parentNode);
    public static event OnConditionLineRemoveClick OnConditionLineRemoveClicked;

    public delegate void OnConditionLineUpdate(Condition condition, int index);
    public static event OnConditionLineUpdate OnConditionLineUpdated;

    private string lineId;
    private GameObject parentNode;
    private Condition conditionOfLine;

    public Dropdown conditionInitiator;
    public Dropdown agentSpecificatorSelector;

    public Button traitSelector;
    public Text traitText;
    private bool selectingTrait;
    private Trait selectedTrait;
    private TraitSelectionTool traitSelectionTool;

    public Button characterSelector;
    public Text characterText;
    private bool selectingCharacter;
    private CharacterStateController selectedCharacter;
    private CharacterSelectionTool characterSelectionTool;

    public Button attributeSelector;
    public Text attributeText;
    private bool selectingAttribute;
    private Attribute selectedAttribute;
    private AttributeSelectionTool attributeSelectionTool;

    // Dropdowns which every selector for every possible type of initiator:
    public Dropdown statusRangeDropdown;
    public Dropdown numericSelectorDropdown;
    public Dropdown traitDropdown;
    public Dropdown eventFlaggedDropdown;
    public Dropdown locationDropdown;
    public Dropdown timeDropdown;
    public Dropdown relationshipDropdown;

    //Inputs for numerical values:
    public InputField firstNumericInput;
    public InputField secondNumericInput;

    public HorizontalLayoutGroup horizontalLayoutGroup;

    private void Start() {
        TraitSelectionTool.OnToolFinished += SelectedTrait;
        CharacterSelectionTool.OnToolFinished += SelectedCharacter;
        AttributeSelectionTool.OnToolFinished += SelectedAttribute;
    }

    private void OnDestroy() {
        TraitSelectionTool.OnToolFinished -= SelectedTrait;
        CharacterSelectionTool.OnToolFinished -= SelectedCharacter;
        AttributeSelectionTool.OnToolFinished -= SelectedAttribute;
    }

    public void OnStartUpConditionNode(GameObject parent, CharacterSelectionTool characterSelectionTool, TraitSelectionTool traitSelectionTool, AttributeSelectionTool attributeSelectionTool) {
        this.conditionOfLine = new Condition();
        this.parentNode = parent;

        this.characterSelectionTool = characterSelectionTool;
        this.attributeSelectionTool = attributeSelectionTool;
        this.traitSelectionTool = traitSelectionTool;
        this.lineId = System.DateTime.Now.Ticks.ToString();

        this.RenderConditionLine();
    }

    public void OnConditionInitiatorSelected() {
        //Since the initiator is the start of everything, reset the  entire line while at it
        this.conditionOfLine.initiator = (Condition.Initiator)this.conditionInitiator.value;
        this.agentSpecificatorSelector.options = new List<Dropdown.OptionData>();

        switch (this.conditionOfLine.initiator) {
            case Condition.Initiator.STATUS_RANGE:
            case Condition.Initiator.ATTRIBUTE_RANGE:
            case Condition.Initiator.LOCATION:
            case Condition.Initiator.TRAIT:
                this.agentSpecificatorSelector.AddOptions(new List<Dropdown.OptionData>() {
                    new Dropdown.OptionData(){text = "Agent"},
                    new Dropdown.OptionData(){text = Condition.Agent.SELF.ToString()},
                    new Dropdown.OptionData(){text = Condition.Agent.TARGET.ToString()},
                    new Dropdown.OptionData(){text = Condition.Agent.SPECIFIC.ToString()}
                });

                break;
            case Condition.Initiator.EVENT_FLAGGED:
                this.agentSpecificatorSelector.AddOptions(new List<Dropdown.OptionData>() {
                    new Dropdown.OptionData(){text = "Agent"},
                    new Dropdown.OptionData(){text = Condition.Agent.SELF.ToString()},
                    new Dropdown.OptionData(){text = Condition.Agent.TARGET.ToString()},
                    new Dropdown.OptionData(){text = Condition.Agent.GLOBAL.ToString()}
                });
                break;
            //If the specificator is uncesscessary, just skip to next condition line step.
            case Condition.Initiator.TIME:
                this.conditionOfLine.agent = Condition.Agent.GLOBAL;

                break;
        }

        this.RenderConditionLine();
    }

    public void OnAgentSpecificatorSelected() {
        this.conditionOfLine.agent = (Condition.Agent)this.agentSpecificatorSelector.value;

        switch (this.conditionOfLine.agent) {
            case Condition.Agent.SELF:
            case Condition.Agent.TARGET:
            case Condition.Agent.GLOBAL:
                //Nothing more to do after estabilishing the specificator as Target/Self/Global
                break;
            case Condition.Agent.SPECIFIC:
                //TODO: Allow selection of specifc id/flag on this choice.
                switch (this.conditionOfLine.initiator) {
                    case Condition.Initiator.STATUS_RANGE:
                    case Condition.Initiator.TRAIT:
                    case Condition.Initiator.LOCATION:
                        //TODO: Specific specificator for each iniator;
                        break;
                }
                break;
        }

        this.RenderConditionLine();
    }

    private void RenderConditionLine() {

        //Check if condition initiator is set, if not remove the next step value (Agent)
        if (this.conditionOfLine.initiator != Condition.Initiator.UNDEFINED) {
            this.agentSpecificatorSelector.gameObject.SetActive(this.conditionOfLine.initiator != Condition.Initiator.TIME);
        }
        else {
            this.conditionOfLine.agent = Condition.Agent.UNDEFINED;
            this.agentSpecificatorSelector.gameObject.SetActive(false);
        }

        if (this.conditionOfLine.agent != Condition.Agent.UNDEFINED) {
            this.ShowConditionSetter(this.conditionOfLine);
        }
        else {
            this.ResetConditionSettersOfCondition();
        }
    }

    private void ShowConditionSetter(Condition condition) {
        switch (condition.initiator) {
            case Condition.Initiator.STATUS_RANGE:
                this.statusRangeDropdown.gameObject.SetActive(true);
                this.numericSelectorDropdown.gameObject.SetActive(true);
                break;
            case Condition.Initiator.ATTRIBUTE_RANGE:
                this.attributeSelector.gameObject.SetActive(true);
                this.numericSelectorDropdown.gameObject.SetActive(true);
                break;
            case Condition.Initiator.TRAIT:
                this.traitDropdown.gameObject.SetActive(true);
                break;
            case Condition.Initiator.EVENT_FLAGGED:
                this.eventFlaggedDropdown.gameObject.SetActive(true);
                break;
            case Condition.Initiator.LOCATION:
                this.locationDropdown.gameObject.SetActive(true);
                break;
            case Condition.Initiator.TIME:
                this.timeDropdown.gameObject.SetActive(true);
                break;
            case Condition.Initiator.RELATIONSHIP:
                this.relationshipDropdown.gameObject.SetActive(true);
                break;
        }
    }

    private void ResetConditionSettersOfCondition() {
        this.conditionOfLine.statusRange.selector = Condition.NumericSelector.UNDEFINED;
        this.conditionOfLine.attrRange.selector = Condition.NumericSelector.UNDEFINED;
        this.conditionOfLine.trait.selector = Condition.Trait.Selector.UNDEFINED;
        this.conditionOfLine.eventFlagged.selector = Condition.EventFlagged.Selector.UNDEFINED;
        this.conditionOfLine.location.selector = Condition.Location.Selector.UNDEFINED;
        this.conditionOfLine.time.selector = Condition.Time.Selector.UNDEFINED;
        this.conditionOfLine.relationship.selector = Condition.Relationship.Selector.UNDEFINED;

        this.statusRangeDropdown.gameObject.SetActive(false);
        this.attributeText.text = "Select Attribute";
        this.numericSelectorDropdown.gameObject.SetActive(false);
        this.traitDropdown.gameObject.SetActive(false);
        //this.eventFlaggedDropdown.gameObject.SetActive(false);
        //this.locationDropdown.gameObject.SetActive(false);
        //this.timeDropdown.gameObject.SetActive(false);
        //this.relationshipDropdown.gameObject.SetActive(false);
    }

    public void OnTraitSelection() {
        this.conditionOfLine.trait.selector = (Condition.Trait.Selector)traitDropdown.value;

        switch (this.conditionOfLine.trait.selector) {
            case Condition.Trait.Selector.DONT:
            case Condition.Trait.Selector.HAS:
                this.traitSelector.gameObject.SetActive(true);
                break;
            default:
                this.conditionOfLine.trait.selector = Condition.Trait.Selector.UNDEFINED;
                this.traitSelector.gameObject.SetActive(false);
                break;
        }
    }
    public void OnAttributeSelectorClick() {
        this.selectingAttribute = true;
        this.attributeSelectionTool.OnEnableTool(this.lineId);
    }

    private void SelectedAttribute(string callerId, Attribute attribute) {
        if (this.lineId != callerId) {
            return;
        }

        this.selectedAttribute = attribute == null && this.selectedAttribute != null ? this.selectedAttribute : attribute;
        this.attributeText.text = this.selectedAttribute != null ? this.selectedAttribute.name : "No Selection";
        this.selectingAttribute = false;
    }

    public void OnTraitSelectorClick() {
        this.selectingTrait = true;
        this.traitSelectionTool.OnEnableTool(this.lineId);
    }
    private void SelectedTrait(string callerId, Trait trait) {
        if (this.lineId != callerId) {
            return;
        }

        this.selectedTrait = trait == null && this.selectedTrait != null ? this.selectedTrait : trait;
        this.traitText.text = this.selectedTrait != null ? this.selectedTrait.name : "No Selection";
        this.selectingTrait = false;
    }

    public void OnCharacterSelectorClick() {
        this.selectingCharacter = true;
        this.characterSelectionTool.OnEnableTool(this.lineId);
    }

    private void SelectedCharacter(string callerId, CharacterStateController characterController) {
        if (this.lineId != callerId) {
            return;
        }

        this.selectedCharacter = characterController == null && this.selectedCharacter != null ? this.selectedCharacter : characterController;
        this.characterText.text = this.selectedCharacter != null ? this.selectedCharacter.baseCharacter.name : "No Selection";
        this.selectingTrait = false;
    }

    public void OnNumericSelector() {
        Condition.NumericSelector numericSelector = (Condition.NumericSelector)this.numericSelectorDropdown.value;

        if (this.conditionOfLine.initiator == Condition.Initiator.STATUS_RANGE) {
            this.conditionOfLine.statusRange.selector = numericSelector;
        }
        else {
            this.conditionOfLine.attrRange.selector = numericSelector;
        }

        switch (numericSelector) {
            case Condition.NumericSelector.BETWEEN:
                this.firstNumericInput.gameObject.SetActive(true);
                this.secondNumericInput.gameObject.SetActive(true);
                break;
            case Condition.NumericSelector.SMALLER_THAN:
            case Condition.NumericSelector.BIGGER_THAN:
            case Condition.NumericSelector.EXACTLY:
                this.firstNumericInput.gameObject.SetActive(true);
                this.secondNumericInput.gameObject.SetActive(false);
                break;
            case Condition.NumericSelector.BIGGER_THAN_SELF:
            case Condition.NumericSelector.BIGGER_THAN_TARGET:
                this.firstNumericInput.gameObject.SetActive(false);
                this.secondNumericInput.gameObject.SetActive(false);
                break;
            default :
                this.firstNumericInput.gameObject.SetActive(false);
                this.secondNumericInput.gameObject.SetActive(false);
                break;
        }
    }

    public void OnRemoveConditionClick() {
        ConditionLine.OnConditionLineRemoveClicked?.Invoke(this.gameObject, this.parentNode);
    }

    private void GetCustomSelectorTypeValues(ConditionMapper.StepsMapper customSelectorType) {

    }

    public NodeFeedback CheckValidityOfLine() {
        //TODO: Logic to check if the line is valid before submit
        return new NodeFeedback();
    }
}
