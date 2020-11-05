using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;
using UnityEngine.UI;

public class ModifierEditor : MonoBehaviour
{
    public Dropdown modifierTargetValueDropdown;
    public MultiSelectController multiSelectController;
    // Start is called before the first frame update
    void Start() {
        multiSelectController.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {

    }
    public void OnModifierTargetSet(Dropdown dropdown) {
        switch ((Effect.Target.Type)(dropdown.value + 1)) {
            case Effect.Target.Type.TARGET_ATTRIBUTE:
                PopulateSelectWithEnumValues<Officer.Attribute>();
                break;
            case Effect.Target.Type.TARGET_MONEY_GAIN:

            default:
                break;
        }
    }

    public void OnModiferTypeChanged(Dropdown dropdown) {
    }

    private void PopulateSelectWithEnumValues<T>() {
        List<MultiSelectController.Option> options = new List<MultiSelectController.Option>();

        List<T> attributes = Utils.GetEnumValues<T>();
        attributes.ForEach(modifier => {
            MultiSelectController.Option option = new MultiSelectController.Option((int)(object)modifier, modifier.ToString());

            options.Add(option);
        });
        multiSelectController.OnStartupMultiselect(options);
    }
}
