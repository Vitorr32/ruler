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

    public List<OfficerSprite> officerSprites = new List<OfficerSprite>();
    public List<Sprite> miscellaneousSprites = new List<Sprite>();
    //public List<Sprite> sprites = new List<Sprite>();
    [SerializeField]
    private List<Sprite> rawOfficerSprites = new List<Sprite>();

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        }
        else {
            instance = this;
        }
    }

    public void ParseRawSpritesIntoOfficeSprites() {
        if (rawOfficerSprites.Count == 0) {
            Debug.LogError("There is no Raw Sprites feed to the Store Controller!");
            return;
        }

        rawOfficerSprites.ForEach(rawSprite => {
            officerSprites.Add(new OfficerSprite(rawSprite));
        });
    }

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {

    }
}