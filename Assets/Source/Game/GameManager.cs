using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public List<TextAsset> officerFiles;
    public List<TextAsset> effectFiles;
    public List<TextAsset> traitFiles;

    // Start is called before the first frame update
    void Start() {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update() {

    }
}
