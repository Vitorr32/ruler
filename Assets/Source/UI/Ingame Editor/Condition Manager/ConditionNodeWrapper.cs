using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConditionNodeWrapper : MonoBehaviour
{
    public delegate void OnNodeRemoveClick(GameObject nodeWrapper, GameObject parentNode);
    public static event OnNodeRemoveClick OnNodeRemoveClicked;

    private ConditionTree.Node conditionNode;
    private GameObject parentNode;
    //All of this condition node individual condition lines
    private List<GameObject> conditionLines = new List<GameObject>();
    private List<GameObject> childNodesWrappers = new List<GameObject>();

    //Gameobjects to make the instantiation operations
    public GameObject conditionNodePrefab;
    public GameObject conditionLinePrefab;
    public GameObject nodeConditionsWrapper;
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

    public void OnNodeCreation(GameObject parent) {
        this.conditionNode = new ConditionTree.Node();
        this.parentNode = parent;
    }

    public void OnAddConditionClick() {
        if (this.conditionNode.logicOperator == LogicOperator.IF && this.conditionLines.Count == 1) {
            Debug.LogError("Cant add more than one condition/child node to a if logic operator!");
            return;
        }

        GameObject conditionLine = Instantiate(this.conditionLinePrefab, this.nodeConditionsWrapper.transform);
        conditionLine.GetComponent<ConditionLine>().OnStartUpConditionNode(this);

        this.conditionLines.Add(conditionLine);
    }

    public void OnAddChildNodeClick() {
        if (this.conditionNode.logicOperator == LogicOperator.IF && this.conditionLines.Count == 1) {
            Debug.LogError("Cant add more than one condition/child node to a if logic operator!");
            return;
        }

        GameObject nodeWrapper = Instantiate(this.conditionNodePrefab, this.nodeConditionsWrapper.transform);
        nodeWrapper.GetComponent<ConditionNodeWrapper>().OnNodeCreation(this.gameObject);

        this.childNodesWrappers.Add(nodeWrapper);
    }

    private void OnRemoveChildNodeClick(GameObject nodeWrapper, GameObject parentNode) {
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
        ConditionNodeWrapper.OnNodeRemoveClicked?.Invoke(this.gameObject, this.parentNode);
    }

    public void OnLogicOperatorChange() {
        LogicOperator logicOperator = (LogicOperator)this.logicOperatorDropdown.value;

        this.conditionNode.logicOperator = logicOperator;
    }

}
