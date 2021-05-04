using System.Collections.Generic;
using UnityEngine;

public class ConditionManager : MonoBehaviour
{
    public ConditionTree conditionTree;

    private List<ConditionNodeWrapper> conditionNodeWrappers = new List<ConditionNodeWrapper>();
    private ConditionNodeWrapper root;

    public GameObject conditionNodesWrapper;
    public ConditionNodeWrapper conditionNodePrefab;

    public void OnConditionAddClick() {
        if (this.conditionTree == null || this.conditionTree.root == null) {
            this.OnConditionConfirmation();
            return;
        }

        this.OnNodeAddition();
    }

    private void OnConditionConfirmation() {
        this.conditionTree = new ConditionTree();

        //Initiante a new conditions node on the wrapper
        ConditionNodeWrapper nodeWrapper = Instantiate<ConditionNodeWrapper>(conditionNodePrefab, conditionNodesWrapper.transform);
        nodeWrapper.OnNodeCreation(null);

        this.root = nodeWrapper;
    }

    private void OnNodeAddition() {
        ConditionNodeWrapper nodeWrapper = Instantiate<ConditionNodeWrapper>(conditionNodePrefab, conditionNodesWrapper.transform);
        nodeWrapper.OnNodeCreation(this.root);

        this.conditionNodeWrappers.Add(nodeWrapper);
    }


}
