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
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {

    }
    public void OnModifierTargetSet(ModifierTarget modifierTarget) {
        switch (modifierTarget) {
            case ModifierTarget.AGAINST_CHARACTER:

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
