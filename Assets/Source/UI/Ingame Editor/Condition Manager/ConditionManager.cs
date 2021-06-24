using System.Collections.Generic;
using UnityEngine;

public class ConditionManager : MonoBehaviour
{
    public ConditionTree conditionTree;

    private List<ConditionNodeWrapper> conditionNodeWrappers = new List<ConditionNodeWrapper>();
    private ConditionNodeWrapper root;

    public GameObject conditionNodesWrapper;
    public ConditionNodeWrapper conditionNodePrefab;

    //Tools for selection of more specific condition aspects
    public CharacterSelectionTool characterSelectionTool;
    public TraitSelectionTool traitSelectionTool;
    public AttributeSelectionTool attributeSelectionTool;

    public void OnConditionAddClick() {
        if (this.root == null) {
            this.OnConditionConfirmation();
            return;
        }

        Debug.LogError("The tree already has a root configured!");
    }

    private void OnConditionConfirmation() {
        this.conditionTree = new ConditionTree();

        //Initiante a new conditions node on the wrapper
        ConditionNodeWrapper nodeWrapper = Instantiate(conditionNodePrefab, conditionNodesWrapper.transform);
        nodeWrapper.OnNodeCreation(null, this.characterSelectionTool, this.traitSelectionTool, this.attributeSelectionTool);

        this.root = nodeWrapper;
    }

    private void OnConditionUpdated() {

    }
}
