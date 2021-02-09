using System;
using UnityEngine;
using UnityEngine.UI;

public class ConditionLine : MonoBehaviour
{
    public bool active;

    private ConditionMapper.StepsMapper stepsMapper;

    public Dropdown logicOperator;
    public Dropdown conditionInitiator;

    public Dropdown customSelectorDropdown;
    public Dropdown customValueDropdown;
    public Dropdown customConditionDropdown;

    public Input firstNumericInput;
    public Input secondNumericInput;

    void Start() {

    }

    public void OnLogicOperatorSelected() {
        this.stepsMapper.logicOperator = (LogicOperator)this.logicOperator.value;
    }
    public void OnConditionInitiatorSelected() {
        //this.stepsMapper.conditionInitiator = (Condition)this.conditionInitiator.value;

        //this.stepsMapper = ConditionMapper.getNextStepOfCondition(this.stepsMapper);

        //this.RenderTheCurrentStepsMapperState(this.stepsMapper);
    }

    private void RenderTheCurrentStepsMapperState(ConditionMapper.StepsMapper stepsMapper) {
        if (stepsMapper.customSelectorType == null) {

        }
    }

    private void GetCustomSelectorTypeValues(ConditionMapper.StepsMapper customSelectorType) {

    }
}
