using System.Collections.Generic;
using UnityEngine;

public class EffectEditor : MonoBehaviour
{

    private List<Effect> effects = new List<Effect>();
    public GameObject effectListGO;

    public ModifierEditor modifierEditor;

    public List<ActionableSelector> effectListItemPrefabPool = new List<ActionableSelector>();

    // Start is called before the first frame update
    void Start() {
        //Deactive every prefab on the list
        effectListItemPrefabPool.ForEach(prefab => prefab.gameObject.SetActive(false));

        ModifierEditor.OnModifierEditorEnded += OnEffectHasSucessfullySubmited;
    }

    // Update is called once per frame
    void Update() {

    }

    void Destroy() {
        ModifierEditor.OnModifierEditorEnded -= OnEffectHasSucessfullySubmited;
    }

    public void OnEffectEditorCalled(int effectToEdit = -1) {
        modifierEditor.StartUpEditor(effectToEdit == -1 ? null : this.effects[effectToEdit]);
    }
    public void OnRemoveEffect(int index) {
        this.effects.RemoveAt(index);

        this.effectListItemPrefabPool[index].OnClearActionableSelector();
    }

    private void OnEffectHasSucessfullySubmited(Effect effect) {
        int index = this.effects.FindIndex(effectInList => effectInList.id == effect.id);

        if (index == -1) {
            this.effects.Add(effect);
            index = this.effects.Count - 1;
        }
        else {
            this.effects[index] = effect;
        }

        this.effectListItemPrefabPool[index].OnSelectorStartup(effect, effect.id, "effectEditor", effect.trigger.type.ToString());
        this.effectListItemPrefabPool[index].gameObject.SetActive(true);
    }
}
