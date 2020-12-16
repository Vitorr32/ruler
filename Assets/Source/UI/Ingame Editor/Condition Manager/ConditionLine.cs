using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConditionLine : MonoBehaviour
{
    // Start is called before the first frame update
    public bool active;

    public Dropdown logicOperator;
    public Dropdown conditionInitiator;

    public List<Dropdown> specifiersDropdowns;

    public List<int> currentArguments = new List<int>() { 0, 0 };

    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void OnLogicOperatorSelected() {
        this.currentArguments[0] = this.logicOperator.value;
        this.conditionInitiator.gameObject.SetActive(true);
    }
    public void OnConditionInitiatorSelected() {
        this.currentArguments[1] = this.conditionInitiator.value;

        ConditionMapper.getNextStepOfCondition(this.currentArguments);
    }
}
