using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public struct GameConstants
    {
        public int effectIdCounter;
    }

    public static GameManager instance;

    public GameConstants constants;
    public List<TextAsset> officerFiles;
    public List<TextAsset> effectFiles;
    public List<TextAsset> traitFiles;
    public List<TextAsset> dialogueFiles;
    public List<TextAsset> characterFiles;

    // Start is called before the first frame update
    void Start() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        }
        else {
            instance = this;
            SceneManager.LoadScene(1);
        }
    }

    // Update is called once per frame
    void Update() {

    }

    private void PopulateGameConstants() {
        this.constants.effectIdCounter = PlayerPrefs.GetInt("effectIdCounter");
    }

    private void SaveCurrentConstantsToPrefs() {
        PlayerPrefs.SetInt("effectIdCounter", this.constants.effectIdCounter);
    }
}
