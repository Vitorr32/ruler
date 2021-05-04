using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConditionNodeWrapper : MonoBehaviour
{
    private ConditionTree.Node conditionNode;
    private ConditionNodeWrapper parentNode;
    //All of this condition node individual condition lines
    private List<ConditionLine> conditionLines = new List<ConditionLine>();

    public ConditionLine conditionLinePrefab;
    public GameObject conditionLinesWrapper;
    public GameObject nodeOptions;

    public Dropdown logicOperatorDropdown;
    public Button addChildNodeButton;
    public Button addConditionButton;
    public Button removedNodeButton;

    // Start is called before the first frame update
    void Start() {

    }

    public void OnNodeCreation(ConditionNodeWrapper parent) {
        this.conditionNode = new ConditionTree.Node();       
        this.parentNode = parent;
        this.ToogleNodeConfigurationButtons(false);

        //If the parent is null, then this node is the root of the tree
        if (this.parentNode == null) {
            this.conditionNode.logicOperator = LogicOperator.IF;
        }
    }

    public void OnAddConditionClick() {
        if (this.conditionNode.logicOperator == LogicOperator.IF && this.conditionNode.conditions.Count == 1) {
            Debug.LogError("Cant add more than one condition to a if logic operator!");
            return;
        }

        ConditionLine conditionLine = Instantiate<ConditionLine>(conditionLinePrefab, conditionLinesWrapper.transform);
        conditionLine.OnStartUpConditionNode(this);

        this.conditionLines.Add(conditionLine);
    }

    public void OnLogicOperatorChange() {
        LogicOperator logicOperator = (LogicOperator)this.logicOperatorDropdown.value;

        this.conditionNode.logicOperator = logicOperator;
        this.ToogleNodeConfigurationButtons(true);
    }

    private void ToogleNodeConfigurationButtons(bool show) {
        this.nodeOptions.SetActive(show);
    }
}
