using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConditionLine : MonoBehaviour
{
    public delegate void OnConditionLineRemoveClick(GameObject conditionLine, GameObject parentNode);
    public static event OnConditionLineRemoveClick OnConditionLineRemoveClicked;

    public delegate void OnConditionLineUpdate(Condition condition, int index);
    public static event OnConditionLineUpdate OnConditionLineUpdated;

    public delegate void OnLayerChange(Condition condition, int identifier, int layer);
    public static event OnLayerChange OnLayerChanged;

    private GameObject parentNode;

    private Condition conditionOfLine;

    public Dropdown conditionInitiator;
    public Dropdown agentSpecificatorSelector;

    public Button traitSelector;
    private bool selectingTrait;
    private Trait selectedTrait;
    public TraitSelectionTool traitSelectionTool;

    public Button characterSelector;
    private bool selectingCharacter;
    private CharacterStateController selectedCharacter;
    public CharacterSelectionTool characterSelectionTool;

    // Dropdowns which every selector for every possible type of initiator:
    public Dropdown numericSelectorDropdown;
    public Dropdown traitDropdown;
    public Dropdown eventFlaggedDropdown;
    public Dropdown locationDropdown;
    public Dropdown timeDropdown;
    public Dropdown relationshipDropdown;

    //Inputs for numerical values:
    public InputField firstNumericInput;
    public InputField secondNumericInput;

    private HorizontalLayoutGroup horizontalLayoutGroup;

    private void Start() {
        TraitSelectionTool.OnToolFinished += SelectedTrait;
        CharacterSelectionTool.OnToolFinished += SelectedCharacter;

        this.horizontalLayoutGroup = this.GetComponent<HorizontalLayoutGroup>();
    }

    private void OnDestroy() {
        TraitSelectionTool.OnToolFinished -= SelectedTrait;
        CharacterSelectionTool.OnToolFinished -= SelectedCharacter;
    }

    public void OnStartUpConditionNode(GameObject parent) {
        this.conditionOfLine = new Condition();
        this.parentNode = parent;

        this.RenderConditionLine();
    }

    public void OnConditionInitiatorSelected() {
        //Since the initiator is the start of everything, reset the  entire line while at it
        this.conditionOfLine.initiator = (Condition.Initiator)this.conditionInitiator.value;
        this.agentSpecificatorSelector.options = new List<Dropdown.OptionData>();

        switch (this.conditionOfLine.initiator) {
            case Condition.Initiator.ATTRIBUTE_RANGE:
            case Condition.Initiator.SKILL_RANGE:
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
                    case Condition.Initiator.ATTRIBUTE_RANGE:
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
            case Condition.Initiator.ATTRIBUTE_RANGE:
            case Condition.Initiator.SKILL_RANGE:
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
        this.conditionOfLine.attributeRange.selector = Condition.NumericSelector.UNDEFINED;
        this.conditionOfLine.skillRange.selector = Condition.NumericSelector.UNDEFINED;
        this.conditionOfLine.trait.selector = Condition.Trait.Selector.UNDEFINED;
        this.conditionOfLine.eventFlagged.selector = Condition.EventFlagged.Selector.UNDEFINED;
        this.conditionOfLine.location.selector = Condition.Location.Selector.UNDEFINED;
        this.conditionOfLine.time.selector = Condition.Time.Selector.UNDEFINED;
        this.conditionOfLine.relationship.selector = Condition.Relationship.Selector.UNDEFINED;

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

    public void OnTraitSelectorClick() {
        this.selectingTrait = true;
        this.traitSelectionTool.gameObject.SetActive(true);
    }

    public void OnNumericSelector() {
        Condition.NumericSelector numericSelector = (Condition.NumericSelector)this.numericSelectorDropdown.value;

        if (this.conditionOfLine.initiator == Condition.Initiator.ATTRIBUTE_RANGE) {
            this.conditionOfLine.attributeRange.selector = numericSelector;
        }
        else {
            this.conditionOfLine.skillRange.selector = numericSelector;
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
            default:
                this.firstNumericInput.gameObject.SetActive(false);
                this.secondNumericInput.gameObject.SetActive(false);
                break;
        }
    }

    public void OnCharacterSelectorClick() {
        this.selectingCharacter = true;
        this.characterSelectionTool.gameObject.SetActive(true);
    }
    private void SelectedTrait(Trait trait) {
        if (!this.selectingTrait) {
            return;
        }

        this.selectedTrait = trait;
        this.traitSelector.GetComponentInChildren<Text>().text = trait.name;
        this.selectingTrait = false;
    }

    private void SelectedCharacter(CharacterStateController characterController) {
        if (!this.selectingCharacter) {
            return;
        }

        this.selectedCharacter = characterController;
        this.characterSelector.GetComponentInChildren<Text>().text = characterController.baseCharacter.name + " " + characterController.baseCharacter.surname;
        this.selectingTrait = false;
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
