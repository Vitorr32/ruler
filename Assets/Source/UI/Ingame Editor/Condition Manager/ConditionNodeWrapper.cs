using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public struct NodeFeedback
{
    public bool valid;
    public string message;
    public List<NodeFeedback> childrenFeedback;
}

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
    public GameObject nodeConditionsWrapper;
    public GameObject nodeOptions;

    //Tools for selection of more specific condition aspects
    private CharacterSelectionTool characterSelectionTool;
    private TraitSelectionTool traitSelectionTool;
    private AttributeSelectionTool attributeSelectionTool;

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

    public void OnNodeCreation(ConditionNodeWrapper parent, CharacterSelectionTool characterSelectionTool, TraitSelectionTool traitSelectionTool, AttributeSelectionTool attributeSelectionTool) {
        this.conditionNode = new ConditionTree.Node();
        this.parentNode = parent;
        this.characterSelectionTool = characterSelectionTool;
        this.attributeSelectionTool = attributeSelectionTool;
        this.traitSelectionTool = traitSelectionTool;
    }

    public void OnAddConditionClick() {
        if (this.conditionNode.logicOperator == LogicOperator.IF && (this.conditionLines.Count == 1 || this.childNodesWrappers.Count == 1)) {
            Debug.LogError("Cant add more than one condition/child node to a if logic operator!");
            return;
        }

        ConditionLine conditionLine = Instantiate(this.conditionLinePrefab, this.nodeConditionsWrapper.transform);
        conditionLine.OnStartUpConditionNode(this, this.characterSelectionTool, this.traitSelectionTool, this.attributeSelectionTool);

        this.conditionLines.Add(conditionLine);
    }

    public void OnAddChildNodeClick() {
        if (this.conditionNode.logicOperator == LogicOperator.IF && (this.conditionLines.Count == 1 || this.childNodesWrappers.Count == 1)) {
            Debug.LogError("Cant add more than one condition/child node to a if logic operator!");
            return;
        }

        ConditionNodeWrapper nodeWrapper = Instantiate(this.conditionNodePrefab, this.nodeConditionsWrapper.transform);
        nodeWrapper.OnNodeCreation(this, this.characterSelectionTool, this.traitSelectionTool, this.attributeSelectionTool);

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

    public NodeFeedback OnCheckNode() {
        NodeFeedback feedback = new NodeFeedback();

        this.conditionLines.ForEach(conditionLine => {
            conditionLine.GetComponent<ConditionLine>().CheckValidityOfLine();
        });

        return feedback;
    }

    public ConditionTree.Node GetStrutctureNodeTreeLeaf() {
        this.conditionNode.conditions = this.conditionLines.Select(line => line.GetLineCondition()).ToList();
        this.conditionNode.children = this.childNodesWrappers.Select(node => node.GetStrutctureNodeTreeLeaf()).ToList();

        return this.conditionNode;
    }

}
