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
        feedback.text = "Processing Characters Images...";

        StoreController.instance.ParseRawSpritesIntoOfficeSprites();

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
                trait.uEffects.Add(StoreController.instance.effects.First(effect => effect.id == trait.effects[j].ToString()));
            }

            trait.sprite = StoreController.instance.miscellaneousSprites.Find(sprite => sprite.name == trait.spriteName);

            StoreController.instance.traits.Add(trait);
        }

        feedback.text = "Importing Skill Tree from JSON";

        JSONController<Attribute> skillReader = new JSONController<Attribute>();
        yield return skillReader.ParseFileListIntoType(GameManager.instance.attributesFiles);
        StoreController.instance.attributes = skillReader.resultList;

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

            controller.officerSprite = new List<OfficerSprite>() {
                StoreController.instance.officerSprites.Find(sprite => sprite.filename == ("o_" + officer.id))
            };

            StoreController.instance.officers.Add(controller);

            feedback.text = "Importing Officers : " + (i + 1) + "/" + officerReader.resultList.Count;
        }

        feedback.text = "Import Characters JSON";

        JSONController<Character> characterReader = new JSONController<Character>();
        yield return characterReader.ParseFileListIntoType(GameManager.instance.characterFiles);

        for (int i = 0; i < characterReader.resultList.Count; i++) {
            Character character = characterReader.resultList[i];

            CharacterStateController characterController = new CharacterStateController(character);

            characterController.charSprites = new List<OfficerSprite>() {
                StoreController.instance.officerSprites.Find(sprite => sprite.filename == ("o_" + character.id))
            };

            StoreController.instance.characterControllers.Add(characterController);

            feedback.text = "Importing Officers : " + (i + 1) + "/" + officerReader.resultList.Count;
        }

        feedback.text = "Instanciating Level...";

        AsyncOperation levelLoad = SceneManager.LoadSceneAsync(3);

        while (levelLoad.progress < 1) {
            proguessBar.fillAmount = levelLoad.progress;
            yield return new WaitForEndOfFrame();
        }
    }
}
