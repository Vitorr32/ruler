using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionNodeWrapper : MonoBehaviour
{
    private ConditionTree.Node conditionNode;

    public ConditionLine conditionLinePrefab;
    public GameObject conditionLinesWrapper;

    // Start is called before the first frame update
    void Start() {

    }

    public ref ConditionTree.Node OnNodeCreation() {
        this.conditionNode = new ConditionTree.Node();

        return ref this.conditionNode;
    }

    public void OnAddConditionClick() {
        if (this.conditionNode.logicOperator == LogicOperator.IF && this.conditionNode.conditions.Count == 1) {
            Debug.LogError("Cant add more than one condition to a if logic operator!");
            return;
        }
    }

    private void SetInitialVisualState() {

    }
}
