using UnityEngine;

public class ConditionManager : MonoBehaviour
{
    public ConditionTree conditionTree;

    public GameObject conditionNodesWrapper;
    public ConditionNodeWrapper conditionNodePrefab;

    public void OnConditionConfirmation() {
        this.conditionTree = new ConditionTree();

        //Initiante a new conditions node on the wrapper
        ConditionNodeWrapper nodeWrapper = Instantiate<ConditionNodeWrapper>(conditionNodePrefab, conditionNodesWrapper.transform);
        this.conditionTree.root = nodeWrapper.OnNodeCreation();
    }
}
