using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;
using UnityEngine.UI;

public enum ModifierTarget
{
    AGAINST_CHARACTER,
    AGAINST_NATIONALITY,
    AGAINST_TRAIT
}
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
        switch ((ModifierTarget)dropdown.value) {
            case ModifierTarget.AGAINST_CHARACTER:
                List<MultiSelectController.Option> options = new List<MultiSelectController.Option>();

                List<Effect.Restriction.Type> modifiers = Utils.GetEnumValues<Effect.Restriction.Type>();
                modifiers.ForEach(modifier => {
                    options.Add(new MultiSelectController.Option((int)modifier, EnumToString.getStringOfRestrictionsEnum(modifier)));
                });
                multiSelectController.OnStartupMultiselect(options, "value", "label");
                //TODO MODIFY SELECTION BASED IN TARGET
                break;
            case ModifierTarget.AGAINST_NATIONALITY:
                break;
            case ModifierTarget.AGAINST_TRAIT:
                break;
        }
    }

    public void OnModiferTypeChanged(Dropdown dropdown) {
        Debug.Log(dropdown.value);
    }
}
