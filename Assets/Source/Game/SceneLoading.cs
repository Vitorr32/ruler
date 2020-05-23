using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoading : MonoBehaviour
{
    GameManager gameManager;
    StoreController store;
    // Start is called before the first frame update
    void Start() {
        gameManager = FindObjectOfType<GameManager>();
        store = FindObjectOfType<StoreController>();

        StartCoroutine(LoadAsyncDependecies());
    }

    IEnumerator LoadAsyncDependecies() {
        Debug.Log("Yolo");
        JSONController<Officer> officerReader = new JSONController<Officer>();
        yield return officerReader.ParseFileListIntoType(gameManager.officerFiles);
        store.officers = officerReader.resultList;

        Debug.Log(store.officers);

        //JSONController<Trait> traitReader = new JSONController<Trait>();
        //yield return traitReader.ParseFileListIntoType(gameManager.officerFiles);
        //store.traits = traitReader.resultList;

        //Debug.Log(store.traits);
    }
}
