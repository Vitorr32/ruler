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
    }

    // Update is called once per frame
    void Update() {

    }
    public void OnModifierTargetSet(Dropdown dropdown) {
        switch ((Effect.Restriction.Type)dropdown.value) {
            case Effect.Restriction.Type.AGAINST_RACE:
                List<MultiSelectController.Option> options = new List<MultiSelectController.Option>();

                List<Officer.Race> modifiers = Utils.GetEnumValues<Officer.Race>();
                modifiers.ForEach(modifier => {
                    MultiSelectController.Option option = new MultiSelectController.Option((int)modifier, modifier.ToString());

                    options.Add(option);
                });
                multiSelectController.OnStartupMultiselect(options);
                break;
            default:
                break;
        }
    }

    public void OnModiferTypeChanged(Dropdown dropdown) {
    }
}
