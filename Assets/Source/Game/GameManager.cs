using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<TextAsset> officerFiles;
    public List<TextAsset> effectFiles;
    public List<TextAsset> traitFiles;
    public List<TextAsset> dialogueFiles;

    // Start is called before the first frame update
    void Start() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        }
        else {
            instance = this;
            SceneManager.LoadScene(3);
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
