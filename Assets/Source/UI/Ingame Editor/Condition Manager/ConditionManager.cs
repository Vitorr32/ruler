using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionManager : MonoBehaviour
{
    public List<Condition> conditions = new List<Condition>();

    public int numberOfPrefabs = 10;
    public List<ConditionLine> conditionLinesPrefabs;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    public void OnConditionLineAddition() {
        int length = conditions.Count;

        if (length > numberOfPrefabs) {
            Debug.Log("Over the prefabs limit");
            return;
        }

        conditionLinesPrefabs[length].OnStartUpConditionLine();
    }

    private void CalculateLogicTreeLayer(Condition condition) {
        
    }
}
