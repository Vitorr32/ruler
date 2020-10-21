using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectEditor : MonoBehaviour
{

    private List<Effect> effects;
    public GameObject effectListGO;

    public List<ActionableSelector> effectListItemPrefabPool = new List<ActionableSelector>();

    // Start is called before the first frame update
    void Start() {
        //Deactive every prefab on the list
        effectListItemPrefabPool.ForEach(prefab => prefab.gameObject.SetActive(false));
    }

    // Update is called once per frame
    void Update() {

    }

    public void startUpEffectAddition() {

    }

    void onAddEffect(Effect effect) {
        this.effects.Add(effect);
    }

    void onRemoveEffect() {

    }
}
