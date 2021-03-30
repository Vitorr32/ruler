using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConditionLine : MonoBehaviour
{
    public bool active;

    private Condition conditionOfLine;

    public Dropdown logicOperator;
    public Dropdown conditionInitiator;
    public Dropdown targetSpecificatorSelector;

    // Dropdowns which every selector for every possible type of initiator
    public Dropdown attrRangeDropdown;
    public Dropdown traitDropdown;
    public Dropdown eventFlaggedDropdown;
    public Dropdown locationDropdown;
    public Dropdown timeDropdown;
    public Dropdown relationshipDropdown;

    public Input firstNumericInput;
    public Input secondNumericInput;

    private int layer;

    void Start() {

    }

    public void OnStartUpConditionLine(ConditionLine previousLine = null, int layer = 0) {
        this.layer = layer;
        this.conditionOfLine = new Condition();

        this.RenderConditionLine(this.conditionOfLine);
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
                    new Dropdown.OptionData(){text = Condition.Specificator.SELF.ToString()},
                    new Dropdown.OptionData(){text = Condition.Specificator.TARGET.ToString()},
                    new Dropdown.OptionData(){text = Condition.Specificator.SPECIFIC.ToString()}
                });

                break;
            case Condition.Initiator.EVENT_FLAGGED:
                this.targetSpecificatorSelector.AddOptions(new List<Dropdown.OptionData>() {
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
                break;
        }
    }

    private void RenderConditionLine(Condition condition) {
        if (this.conditionOfLine.logicOperator != Condition.LogicOperator.UNDEFINED) {
            this.RenderConditionOperator(condition);
            this.conditionInitiator.gameObject.SetActive(true);
        }

        if (this.conditionOfLine.initiator != Condition.Initiator.UNDEFINED) {
            this.targetSpecificatorSelector.gameObject.SetActive(this.conditionOfLine.initiator != Condition.Initiator.TIME);
        }

        if (this.conditionOfLine.specificator != Condition.Specificator.UNDEFINED) {
            this.ShowConditionSetter(condition);
        }
    }

    private void RenderConditionOperator(Condition condition) {
        //Give a little padding to the line to the left to better visualization of dependent lines of conditions
        switch (condition.logicOperator) {
            case Condition.LogicOperator.AND:
                this.gameObject.GetComponent<HorizontalLayoutGroup>().padding.left = (this.layer * 20) + 20;
                break;
            case Condition.LogicOperator.OR:
                this.gameObject.GetComponent<HorizontalLayoutGroup>().padding.left = this.layer * 20;
                break;
            case Condition.LogicOperator.IF:
                this.gameObject.GetComponent<HorizontalLayoutGroup>().padding.left = 0;
                break;
        }
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

    private void GetCustomSelectorTypeValues(ConditionMapper.StepsMapper customSelectorType) {

    }
}
