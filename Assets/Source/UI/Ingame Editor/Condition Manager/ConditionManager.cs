using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionManager : MonoBehaviour
{
    public struct ConditionManagerNodeMetaData
    {
        public Condition condition;
        public int identifier;
        public int indexOnList;
        public int layer;
        public bool isRoot;
    }

    public List<Condition> conditions = new List<Condition>();
    public ConditionTree conditionTree;

    public int numberOfPrefabs = 10;
    public List<ConditionLine> conditionLinesPrefabs;

    private int numberOfLines = 0;
    private int identifierCounter = 0;

    // Start is called before the first frame update
    void Start() {
        ConditionLine.OnConditionLineUpdated += this.OnConditionNodeUpdated;
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnEnable() {
        this.conditionTree = new ConditionTree();
    }

    public void OnConditionLineAddition() {
        int length = conditions.Count;

        if (length > numberOfPrefabs) {
            Debug.Log("Over the prefabs limit");
            return;
        }

        this.numberOfLines++;

        //ConditionTree.Node newNode = new ConditionTree.Node() {
        //    condition = new Condition(),
        //    children = new List<ConditionTree.Node>()
        //};

        ConditionManagerNodeMetaData newNodeMetaData = new ConditionManagerNodeMetaData() {
            condition = new Condition(),
            identifier = this.identifierCounter,
            indexOnList = length,
            isRoot = this.numberOfLines == 1,
            layer = 0
        };

        //conditionLinesPrefabs[length].OnStartUpConditionLine(ref nodeData);
        this.identifierCounter++;
    }

    public void OnConditionNodeRemoved() {

    }

    private void CalculateLogicTreeLayer(Condition condition) {

    }

    private void OnConditionNodeUpdated(Condition condition, int index) {

    }
}
