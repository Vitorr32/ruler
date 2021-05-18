using System.Collections.Generic;
using UnityEngine;

public class ConditionManager : MonoBehaviour
{
    public ConditionTree conditionTree;

    private List<GameObject> conditionNodeWrappers = new List<GameObject>();
    private GameObject root;

    public GameObject conditionNodesWrapper;
    public GameObject conditionNodePrefab;

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
        GameObject nodeWrapper = Instantiate(conditionNodePrefab, conditionNodesWrapper.transform);
        nodeWrapper.GetComponent<ConditionNodeWrapper>().OnNodeCreation(null);

        this.root = nodeWrapper;
    }
}
