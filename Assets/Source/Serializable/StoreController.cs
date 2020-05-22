using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Store will have all the serializable data of the game, including traits, officers and effects
public class StoreController : MonoBehaviour
{
    static StoreController instance;

    public Effect[] effects;
    public Officer[] officers;
    public Trait[] traits;

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
