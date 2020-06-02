using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoading : MonoBehaviour
{
    public Image proguessBar;
    public Text feedback;
    // Start is called before the first frame update
    void Start() {
        StartCoroutine(LoadAsyncDependecies());
    }

    IEnumerator LoadAsyncDependecies() {
        feedback.text = "Importing effects from JSON";

        JSONController<Effect> effectReader = new JSONController<Effect>();
        yield return effectReader.ParseFileListIntoType(GameManager.instance.effectFiles);
        StoreController.instance.effects = effectReader.resultList;

        feedback.text = "Importing dialogues from JSON";

        JSONController<Dialogue> dialogueReader = new JSONController<Dialogue>();
        yield return dialogueReader.ParseFileListIntoType(GameManager.instance.dialogueFiles);
        StoreController.instance.dialogues = dialogueReader.resultList;

        feedback.text = "Importing traits from JSON";

        JSONController<Trait> traitReader = new JSONController<Trait>();
        yield return traitReader.ParseFileListIntoType(GameManager.instance.traitFiles);

        for (int i = 0; i < traitReader.resultList.Count; i++) {
            Trait trait = traitReader.resultList[i];

            for (int j = 0; j < trait.effects.Length; j++) {
                trait.uEffects.Add(StoreController.instance.effects.First(effect => effect.id == trait.effects[j]));
            }

            StoreController.instance.traits.Add(trait);
        }

        feedback.text = "Importing officers from JSON";

        JSONController<Officer> officerReader = new JSONController<Officer>();
        yield return officerReader.ParseFileListIntoType(GameManager.instance.officerFiles);

        for (int i = 0; i < officerReader.resultList.Count; i++) {
            Officer officer = officerReader.resultList[i];

            OfficerController controller = new OfficerController();
            if (i == 0) {
                controller.isPlayer = true;
            }
            controller.StartUpController(officer);
            StoreController.instance.officers.Add(controller);

            feedback.text = "Importing Officers : " + (i + 1) + "/" + officerReader.resultList.Count;
        }

        AsyncOperation levelLoad = SceneManager.LoadSceneAsync(2);

        while (levelLoad.progress < 1) {
            proguessBar.fillAmount = levelLoad.progress;
            yield return new WaitForEndOfFrame();
        }
    }
}
