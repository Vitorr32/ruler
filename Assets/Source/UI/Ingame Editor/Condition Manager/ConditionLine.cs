using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConditionLine : MonoBehaviour
{
    public delegate void OnConditionLineUpdate(Condition condition, int index);
    public static event OnConditionLineUpdate OnConditionLineUpdated;

    public delegate void OnLayerChange(Condition condition, int identifier, int layer);
    public static event OnLayerChange OnLayerChanged;

    public bool active;
    private bool isRoot;
    private int layer;
    private int identifier;
    //Since it's a condition tree, the tree node should know it's parent
    private Condition parent;

    private Condition conditionOfLine;

    public Button deeperIndentButton;
    public Button higherIndentButton;

    public Dropdown logicOperator;
    public Dropdown conditionInitiator;
    public Dropdown targetSpecificatorSelector;

    public Button traitSelector;
    private bool selectingTrait;
    private Trait selectedTrait;
    public TraitSelectionTool traitSelectionTool;

    public Button characterSelector;
    private bool selectingCharacter;
    private CharacterStateController selectedCharacter;
    public CharacterSelectionTool characterSelectionTool;

    // Dropdowns which every selector for every possible type of initiator:
    public Dropdown attrRangeDropdown;
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
        //SHOULD REMOVE NEXT LINE: TESTING
        this.OnStartUpConditionLine(true, 0);
    }

    private void OnDestroy() {
        TraitSelectionTool.OnToolFinished -= SelectedTrait;
        CharacterSelectionTool.OnToolFinished -= SelectedCharacter;
    }

    public void OnStartUpConditionNode() {

    }

    public void OnStartUpConditionLine(bool isRoot, int index, ConditionLine previousLine = null) {
        this.isRoot = isRoot;
        this.identifier = index;
        this.conditionOfLine = new Condition();

        this.RenderConditionLine(this.conditionOfLine);
    }

    public void OnUpdateParentNode(Condition parent) {
        this.parent = parent;
    }

    public void OnIndentButtonClicked(bool deeperLayer) {
        this.layer = this.layer + (deeperLayer ? 1 : -1);

        if (this.layer == 0 || isRoot) {
            this.higherIndentButton.gameObject.SetActive(false);
        }
        else {
            this.higherIndentButton.gameObject.SetActive(true);
        }

        if (this.layer == Constants.MAX_CONDITION_LAYERS || isRoot) {
            this.deeperIndentButton.gameObject.SetActive(false);
        }
        else {
            this.deeperIndentButton.gameObject.SetActive(true);
        }

        this.RenderConditionLine(this.conditionOfLine);
        ConditionLine.OnLayerChanged?.Invoke(this.conditionOfLine, this.identifier, this.layer);
    }

    public void OnLogicOperatorSelected() {
        if (this.logicOperator.value == 0) {
            return;
        }

        if (this.conditionOfLine != null) {
            this.conditionOfLine.logicOperator = (Condition.LogicOperator)this.logicOperator.value;
        }
        else {
            this.conditionOfLine = new Condition() {
                logicOperator = (Condition.LogicOperator)this.logicOperator.value
            };
        }

        this.conditionInitiator.gameObject.SetActive(true);
    }

    public void OnConditionInitiatorSelected() {
        //Since the initiator is the start of everything, reset the  entire line while at it
        this.conditionOfLine.initiator = (Condition.Initiator)this.conditionInitiator.value;
        this.targetSpecificatorSelector.options = new List<Dropdown.OptionData>();

        switch (this.conditionOfLine.initiator) {
            case Condition.Initiator.ATTRIBUTE_RANGE:
            case Condition.Initiator.LOCATION:
            case Condition.Initiator.TRAIT:
                this.targetSpecificatorSelector.AddOptions(new List<Dropdown.OptionData>() {
                    new Dropdown.OptionData(){text = "Specificator"},
                    new Dropdown.OptionData(){text = Condition.Specificator.SELF.ToString()},
                    new Dropdown.OptionData(){text = Condition.Specificator.TARGET.ToString()},
                    new Dropdown.OptionData(){text = Condition.Specificator.SPECIFIC.ToString()}
                });

                break;
            case Condition.Initiator.EVENT_FLAGGED:
                this.targetSpecificatorSelector.AddOptions(new List<Dropdown.OptionData>() {
                    new Dropdown.OptionData(){text = "Specificator"},
                    new Dropdown.OptionData(){text = Condition.Specificator.SELF.ToString()},
                    new Dropdown.OptionData(){text = Condition.Specificator.TARGET.ToString()},
                    new Dropdown.OptionData(){text = Condition.Specificator.GLOBAL.ToString()}
                });
                break;
            //If the specificator is uncesscessary, just skip to next condition line step.
            case Condition.Initiator.TIME:
                this.conditionOfLine.specificator = Condition.Specificator.GLOBAL;

                break;
        }

        this.RenderConditionLine(this.conditionOfLine);
    }

    public void OnSpecificatorSelected() {
        this.conditionOfLine.specificator = (Condition.Specificator)this.targetSpecificatorSelector.value;

        switch (this.conditionOfLine.specificator) {
            case Condition.Specificator.SELF:
            case Condition.Specificator.TARGET:
            case Condition.Specificator.GLOBAL:
                //Nothing more to do after estabilishing the specificator as Target/Self/Global
                break;
            case Condition.Specificator.SPECIFIC:
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

        this.RenderConditionLine(this.conditionOfLine);
    }

    private void RenderConditionLine(Condition condition) {
        if (this.isRoot) {
            this.deeperIndentButton.gameObject.SetActive(false);
            this.higherIndentButton.gameObject.SetActive(false);
        }

        this.RenderIndentationSpace(this.layer);

        if (condition.logicOperator != Condition.LogicOperator.UNDEFINED) {
            //this.RenderConditionOperator(condition);
            this.conditionInitiator.gameObject.SetActive(true);
        }

        if (condition.initiator != Condition.Initiator.UNDEFINED) {
            this.targetSpecificatorSelector.gameObject.SetActive(this.conditionOfLine.initiator != Condition.Initiator.TIME);
        }
        else {

        }

        if (this.conditionOfLine.specificator != Condition.Specificator.UNDEFINED) {
            this.ShowConditionSetter(condition);
        }
    }

    private void RenderIndentationSpace(int layer) {
        //Give a little padding to the line to the left to better visualization of dependent lines of conditions
        this.horizontalLayoutGroup.padding.left = layer * 20;
    }

    private void ShowConditionSetter(Condition condition) {
        switch (condition.initiator) {
            case Condition.Initiator.ATTRIBUTE_RANGE:
                this.attrRangeDropdown.gameObject.SetActive(true);
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

    public void OnAttributeRangeSelection() {
        this.conditionOfLine.attributeRange.selector = (Condition.AttributeRange.Selector)this.attrRangeDropdown.value;

        switch (this.conditionOfLine.attributeRange.selector) {
            case Condition.AttributeRange.Selector.BETWEEN:
                this.firstNumericInput.gameObject.SetActive(true);
                this.secondNumericInput.gameObject.SetActive(true);
                break;
            case Condition.AttributeRange.Selector.SMALLER_THAN:
            case Condition.AttributeRange.Selector.BIGGER_THAN:
            case Condition.AttributeRange.Selector.EXACTLY:
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

    private void GetCustomSelectorTypeValues(ConditionMapper.StepsMapper customSelectorType) {

    }
}
