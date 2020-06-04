using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

//Store will have all the serializable data of the game, including traits, officers and effects
public class StoreController : MonoBehaviour
{
    public static StoreController instance;

    public List<Effect> effects = new List<Effect>();
    public List<OfficerController> officers = new List<OfficerController>();
    public List<Trait> traits = new List<Trait>();
    public List<Dialogue> dialogues = new List<Dialogue>();
    public List<Sprite> sprites = new List<Sprite>();

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        }
        else {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
