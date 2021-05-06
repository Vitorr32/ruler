using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConditionNodeWrapper : MonoBehaviour
{
    public delegate void OnNodeRemoveClick(ConditionNodeWrapper nodeWrapper, ConditionNodeWrapper parentNode);
    public static event OnNodeRemoveClick OnNodeRemoveClicked;

    private ConditionTree.Node conditionNode;
    private ConditionNodeWrapper parentNode;
    //All of this condition node individual condition lines
    private List<ConditionLine> conditionLines = new List<ConditionLine>();
    private List<ConditionNodeWrapper> childNodesWrappers = new List<ConditionNodeWrapper>();

    //Gameobjects to make the instantiation operations
    public ConditionNodeWrapper conditionNodePrefab;
    public ConditionLine conditionLinePrefab;
    public GameObject conditionNodesWrapper;
    public GameObject conditionLinesWrapper;
    public GameObject nodeOptions;

    //References to the elements of the UI Element
    public Dropdown logicOperatorDropdown;
    public Button addChildNodeButton;
    public Button addConditionButton;
    public Button removedNodeButton;

    // Start is called before the first frame update
    void Start() {
        ConditionNodeWrapper.OnNodeRemoveClicked += this.OnRemoveChildNodeClick;
        ConditionLine.OnConditionLineRemoveClicked += this.OnRemoveConditionLine;
    }

    private void OnDestroy() {
        ConditionNodeWrapper.OnNodeRemoveClicked -= this.OnRemoveChildNodeClick;
        ConditionLine.OnConditionLineRemoveClicked -= this.OnRemoveConditionLine;
    }

    public void OnNodeCreation(ConditionNodeWrapper parent) {
        this.conditionNode = new ConditionTree.Node();
        this.parentNode = parent;
    }

    public void OnAddConditionClick() {
        if (this.conditionNode.logicOperator == LogicOperator.IF && this.conditionLines.Count == 1) {
            Debug.LogError("Cant add more than one condition/child node to a if logic operator!");
            return;
        }

        ConditionLine conditionLine = Instantiate<ConditionLine>(conditionLinePrefab, conditionLinesWrapper.transform);
        conditionLine.OnStartUpConditionNode(this);

        this.conditionLines.Add(conditionLine);
    }

    public void OnAddChildNodeClick() {
        if (this.conditionNode.logicOperator == LogicOperator.IF && this.conditionLines.Count == 1) {
            Debug.LogError("Cant add more than one condition/child node to a if logic operator!");
            return;
        }

        ConditionNodeWrapper nodeWrapper = Instantiate<ConditionNodeWrapper>(conditionNodePrefab, conditionNodesWrapper.transform);
        nodeWrapper.OnNodeCreation(this);

        this.childNodesWrappers.Add(nodeWrapper);
    }

    private void OnRemoveChildNodeClick(ConditionNodeWrapper nodeWrapper, ConditionNodeWrapper parentNode) {
        if (parentNode != this) {
            return;
        }

        int indexOnList = this.childNodesWrappers.FindIndex(childNodeWrapper => childNodeWrapper == nodeWrapper);
        this.childNodesWrappers.RemoveAt(indexOnList);

        Destroy(nodeWrapper.gameObject);
    }

    private void OnRemoveConditionLine(ConditionLine conditionLine, ConditionNodeWrapper parentNode) {
        if (parentNode != this) {
            return;
        }

        int indexOnList = this.conditionLines.FindIndex(conditionLineWrapper => conditionLineWrapper == conditionLine);
        this.conditionLines.RemoveAt(indexOnList);

        Destroy(conditionLine.gameObject);
    }

    public void OnNodeRemoveButtonClick() {
        ConditionNodeWrapper.OnNodeRemoveClicked?.Invoke(this, this.parentNode);
    }

    public void OnLogicOperatorChange() {
        LogicOperator logicOperator = (LogicOperator)this.logicOperatorDropdown.value;

        this.conditionNode.logicOperator = logicOperator;
    }

}
