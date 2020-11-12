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

    public MultiSelectController primaryTargetSelectController;
    public MultiSelectController secondaryTargetSelectController;

    public GameObject valueChangeGO;
    public GameObject absoluteValueChangeGO;
    public Text absoluteValueChangeText;
    public GameObject relativeValueChangeGO;
    public Text relativeValueChangeText;

    // Start is called before the first frame update
    void Start() {
        modifierTargetGameObject.SetActive(false);

        this.DeactivateModifierValue();
        primaryTargetSelectController.gameObject.SetActive(false);
        secondaryTargetSelectController.gameObject.SetActive(false);

        MultiSelectController.onMultiselectChanged += OnMultiSelectChange;
    }

    // Update is called once per frame
    void Update() {

    }

    private void Destroy() {
        MultiSelectController.onMultiselectChanged -= OnMultiSelectChange;
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

    public void OnModifierTargetSet(Dropdown dropdown) {

        Enum.TryParse(dropdown.options[dropdown.value].text, out Effect.Target.Type actionType);
        switch (actionType) {
            case Effect.Target.Type.TARGET_ATTRIBUTE:
                PopulateSelectWithEnumValues<Officer.Attribute>(this.primaryTargetSelectController);
                ActivateAbsoluteChangeInput();
                ActivateRelativeChangeInput();
                break;
            case Effect.Target.Type.TARGET_MONEY_GAIN:

            default:
                break;
        }
    }

    private void PopulateSelectWithEnumValues<T>(MultiSelectController controller) {
        List<MultiSelectController.Option> options = new List<MultiSelectController.Option>();

        List<T> attributes = Utils.GetEnumValues<T>();
        attributes.ForEach(modifier => {
            MultiSelectController.Option option = new MultiSelectController.Option((int)(object)modifier, modifier.ToString());

            options.Add(option);
        });
        controller.OnStartupMultiselect(options);
    }

    private void OnMultiSelectChange(int value, MultiSelectController controller) {
        
    }

    private void ActivateRelativeChangeInput() {
        this.relativeValueChangeGO.SetActive(true);
        this.relativeValueChangeText.text = "";
    }
    private void ActivateAbsoluteChangeInput() {
        this.absoluteValueChangeGO.SetActive(true);
        this.absoluteValueChangeText.text = "";
    }
    private void DeactivateModifierValue() {
        valueChangeGO.SetActive(false);
        this.relativeValueChangeGO.SetActive(false);
        this.absoluteValueChangeGO.SetActive(false);
    }
}
