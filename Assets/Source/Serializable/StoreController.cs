using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

//Store will have all the serializable data of the game, including traits, officers and effects
public class StoreController : MonoBehaviour
{
    static StoreController instance;

    public List<Effect> effects;
    public List<OfficerController> officers = new List<OfficerController>();
    public List<Trait> traits;

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
