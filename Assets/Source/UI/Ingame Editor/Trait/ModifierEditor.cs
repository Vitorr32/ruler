using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;
using UnityEngine.UI;

public class ModifierEditor : MonoBehaviour
{
    public GameObject modifierTargetGameObject;
    public Dropdown modifierTargetValueDropdown;

    public MultiSelectController multiSelectController;
    // Start is called before the first frame update
    void Start() {
        modifierTargetGameObject.SetActive(false);
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
        string[] options;

        modifierTargetGameObject.SetActive(true);
        Enum.TryParse(dropdown.options[dropdown.value].text, out ActionType actionType);
        switch (actionType) {
            case ActionType.ALWAYS_ACTIVE:
                options = new string[] {
                    Effect.Target.Type.TARGET_ATTRIBUTE.ToString(),
                    Effect.Target.Type.TARGET_POPULARITY_GAIN.ToString(),
                    Effect.Target.Type.TARGET_STRESS_GAIN.ToString()
                };
                modifierTargetValueDropdown.options = DropdownCreator.ConvertStringArrayToOptions(options);
                break;

            case ActionType.ON_INTERACTION:
                options = new string[] {
                    Effect.Target.Type.TARGET_INTERACTION.ToString()
                };
                modifierTargetValueDropdown.options = DropdownCreator.ConvertStringArrayToOptions(options);
                break;

            //modifierTargetValueDropdown.options = DropdownCreator.ConvertStringArrayToOptions()
            default:
                modifierTargetGameObject.SetActive(false);
                modifierTargetValueDropdown.options = new List<Dropdown.OptionData>();
                break;
        }
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
