using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoading : MonoBehaviour
{
    GameManager gameManager;
    StoreController store;

    public Image proguessBar;
    public Text feedback;
    // Start is called before the first frame update
    void Start() {
        gameManager = FindObjectOfType<GameManager>();
        store = FindObjectOfType<StoreController>();

        StartCoroutine(LoadAsyncDependecies());
    }

    IEnumerator LoadAsyncDependecies() {
        feedback.text = "Importing officers JSON";

        JSONController<Officer> officerReader = new JSONController<Officer>();
        yield return officerReader.ParseFileListIntoType(gameManager.officerFiles);

        for (int i = 0; i < officerReader.resultList.Count; i++) {
            Officer officer = officerReader.resultList[i];

            OfficerController controller = new OfficerController();
            controller.StartUpController(officer);
            store.officers.Add(controller);

            feedback.text = "Importing Officers : " + (i + 1) + "/" + officerReader.resultList.Count;
        }

        AsyncOperation levelLoad = SceneManager.LoadSceneAsync(2);

        while (levelLoad.progress < 1) {
            proguessBar.fillAmount = levelLoad.progress;
            yield return new WaitForEndOfFrame();
        }
    }
}
