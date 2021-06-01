using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifierEditor : MonoBehaviour
{
    public delegate void OnModiferEditorEnd(Effect effect);
    public static event OnModiferEditorEnd OnModifierEditorEnded;

    public Dropdown modifierTriggerDropdown;

    public GameObject modifierTypeGameObject;
    public Dropdown modifierTypeDropdown;

    public Toggle modifierTargetToogle;

    public MultiSelectController modifierTargetMultiselectController;

    public GameObject valueInputGO;
    public InputField valueInputField;

    public Text summaryText;

    private Effect currentEffect = new Effect();

    // Start is called before the first frame update
    void Start() {
        MultiSelectController.onMultiselectChanged += OnMultiselectEventReceived;

        this.CleanUpEditor();
    }

    void Destroy() {
        MultiSelectController.onMultiselectChanged -= OnMultiselectEventReceived;
    }

    private void OnDisable() {
        this.CleanUpEditor();
        this.GetComponent<PopupWrapper>().HidePopup();
    }

    public void StartUpEditor(Effect toEditEffect = null) {
        if (toEditEffect != null) {
            this.PopulateEffectValuesForEdit(toEditEffect);
        }
        this.gameObject.SetActive(true);
    }
    //The first dropdown, define the type of trigger that can activate this effect
    public void OnTriggerTypeChanged(Dropdown dropdown) {
        this.currentEffect.trigger = (Effect.Trigger)dropdown.value;

        switch (this.currentEffect.trigger) {
            case Effect.Trigger.ON_INTERACTION_END:
            case Effect.Trigger.ON_INTERACTION_START:
            case Effect.Trigger.ON_INTERACTION_TALK_ABOUT:
                this.modifierTargetToogle.gameObject.SetActive(true);
                break;
            default:
                this.modifierTargetToogle.gameObject.SetActive(false);
                break;
        }
        this.modifierTypeGameObject.SetActive(true);

        this.RenderSummaryOfEffect(this.currentEffect);
    }

    //Second dropdown, define what the possible selectable values to be modified in this effect
    public void OnModifierTypeSet(Dropdown dropdown) {
        this.currentEffect.modifier.type = (Modifier.Type)dropdown.value;

        this.PopulateModifierTargetMultiselect(this.currentEffect.modifier);
        this.ActiveInputGameObject();
        this.RenderSummaryOfEffect(this.currentEffect);
    }

    public void OnToogleTargetSelf() {
        this.currentEffect.modifier.targetSelf = this.modifierTargetToogle.isOn;

        this.RenderSummaryOfEffect(this.currentEffect);
    }

    private void OnMultiselectEventReceived(bool isAdding, int value, int identifier, MultiSelectController controller) {
        if (isAdding) {
            this.OnMultiSelectOptionAdded(value, identifier, controller);
        }
        else {
            this.OnMultiselectOptionRemoved(value, identifier, controller);
        }
    }
    //When the add option to the multiselect event is recevied, add it to the effect modifier targets
    private void OnMultiSelectOptionAdded(int value, int identifier, MultiSelectController controller) {
        if (controller != modifierTargetMultiselectController) {
            return;
        }

        int modifierTargetIndex = this.currentEffect.modifier.modifierTargets.FindIndex(target => target == value);

        if (modifierTargetIndex != -1) {
            this.currentEffect.modifier.modifierTargets.RemoveAt(modifierTargetIndex);
        }
        else {
            Debug.LogError("Asked to delete modifier target that is not on the list! value = " + value);
        }

        this.RenderSummaryOfEffect(this.currentEffect);
    }

    private void OnMultiselectOptionRemoved(int value, int identifier, MultiSelectController controller) {
        if (controller != modifierTargetMultiselectController) {
            return;
        }

        int modifierTargetIndex = this.currentEffect.modifier.modifierTargets.FindIndex(target => target == value);

        if (modifierTargetIndex == -1) {
            this.currentEffect.modifier.modifierTargets.Add(value);
        }

        this.RenderSummaryOfEffect(this.currentEffect);
    }

    public void OnSubmitEffectToTrait() {

        if (CheckIfModifierValuesAreValid(this.currentEffect)) {
            this.currentEffect.id = PlayerPrefs.GetInt("effectIdCounter") + 1;
            PlayerPrefs.SetInt("effectIdCounter", this.currentEffect.id);
            OnModifierEditorEnded?.Invoke(this.currentEffect);
            this.CleanUpEditor();

            this.gameObject.GetComponent<PopupWrapper>().HidePopup();
        }
    }

    private bool CheckIfModifierValuesAreValid(Effect effect) {
        return false;
        //switch (target.type) {
        //    case Effect.Trigger.ON_INTERACTION_END:
        //        foreach (int[] argumentList in target.arguments) {
        //            if (argumentList[1] != 0 && argumentList[2] != 0) {
        //                this.ShowErrorMessage("A attribute can't be modified by an absolute and relative value at the same time");
        //                return false;
        //            }
        //        }

        //        return true;
        //}

        //return false;
    }

    public void OnInputValueChanged() {
        if (this.valueInputField.text == "") {
            return;
        }

        double effectiveChange;
        if (Double.TryParse(this.valueInputField.text, out effectiveChange)) {
            this.currentEffect.modifier.effectiveChange = (float)effectiveChange;
        }
        else {
            Debug.LogError("It was not possible to convert the user input into a double number!");
        }

        this.RenderSummaryOfEffect(this.currentEffect);
    }

    private void RenderSummaryOfEffect(Effect effect) {
        this.summaryText.text = Summarizer.SummarizeEffect(effect, true);
    }

    private void ActiveInputGameObject() {
        this.valueInputGO.SetActive(true);
        this.valueInputField.text = "";
    }

    private void DeactivateModifierValue() {
        this.valueInputGO.SetActive(false);
        this.valueInputField.text = "";
    }

    private void CleanUpEditor() {
        this.modifierTypeGameObject.SetActive(false);
        this.modifierTargetMultiselectController.gameObject.SetActive(false);
        this.summaryText.text = "";

        this.modifierTriggerDropdown.GetComponent<DropdownCreator>().ResetDropdownState();
        this.modifierTypeDropdown.GetComponent<DropdownCreator>().ResetDropdownState();

        this.currentEffect = new Effect();
        this.DeactivateModifierValue();
    }
    private void PopulateEffectValuesForEdit(Effect effect) {
        this.currentEffect = effect;

        this.PopulateDropdownValues(this.currentEffect);
        this.PopulateModifierTargetMultiselect(this.currentEffect.modifier);
        this.PopulateInputValues(this.currentEffect.modifier);

        this.RenderSummaryOfEffect(this.currentEffect);
    }

    private void PopulateModifierTargetMultiselect(Modifier modifier) {
        switch (modifier.type) {
            case Modifier.Type.MODIFY_SKILL_POTENTIAL_VALUE:
            case Modifier.Type.MODIFY_SKILL_VALUE:
                this.PopulateMultiSelectWithObject<Attribute>(
                    this.modifierTargetMultiselectController,
                    (int)this.currentEffect.modifier.type,
                    StoreController.instance.skills,
                    "name",
                    "id",
                    this.currentEffect.modifier.modifierTargets.ToArray()
                );
                break;
            case Modifier.Type.MODIFY_PASSIVE_ABSOLUTE_VALUE:
            case Modifier.Type.MODIFY_PASSIVE_RELATIVE_VALUE:
                this.PopulateSelectWithEnumValues<Modifier.PassiveValue>(
                    this.modifierTargetMultiselectController,
                    (int)this.currentEffect.modifier.type,
                    this.currentEffect.modifier.modifierTargets.ToArray()
                );
                this.modifierTargetMultiselectController.gameObject.SetActive(true);
                break;
            default:
                Debug.LogError("Unknown Modifier Type " + modifier.type + " can't be used to populate multiselect");
                this.modifierTargetMultiselectController.gameObject.SetActive(false);
                return;
        }
        this.modifierTargetMultiselectController.gameObject.SetActive(true);
    }

    private void PopulateSelectWithEnumValues<T>(MultiSelectController controller, int identifier, int[] currentlySelected = null) {
        List<MultiSelectController.Option> options = new List<MultiSelectController.Option>();

        List<T> attributes = Utils.GetEnumValues<T>();
        attributes.ForEach(modifier => {
            MultiSelectController.Option option = new MultiSelectController.Option((int)(object)modifier, modifier.ToString());

            options.Add(option);
        });

        controller.OnStartupMultiselect(options, identifier, currentlySelected);
    }

    private void PopulateMultiSelectWithObject<T>(MultiSelectController controller, int identifier, List<T> objects, string labelKey, string valueKey, int[] currentlySelected = null) {
        List<MultiSelectController.Option> options = new List<MultiSelectController.Option>();

        try {
            objects.ForEach(toPopulateObject => {
                object label = toPopulateObject.GetType().GetField(labelKey).GetValue(toPopulateObject);
                object value = toPopulateObject.GetType().GetField(valueKey).GetValue(toPopulateObject);

                MultiSelectController.Option option = new MultiSelectController.Option((int)value, (string)label);
                options.Add(option);
            });

            controller.OnStartupMultiselect(options, identifier, currentlySelected);
        }
        catch (InvalidCastException e) {
            Debug.LogError(e);
            return;
        }
    }

    private void PopulateDropdownValues(Effect effect) {
        //Set the value of the dropdown manually so they can be shown on the Unity dropdown component
        this.modifierTriggerDropdown.SetValueWithoutNotify(this.modifierTriggerDropdown.options.FindIndex(option => option.text == Enum.GetName(typeof(Effect.Trigger), effect.trigger)));
        this.OnTriggerTypeChanged(this.modifierTriggerDropdown);
        this.modifierTypeDropdown.SetValueWithoutNotify(this.modifierTypeDropdown.options.FindIndex(option => option.text == Enum.GetName(typeof(Modifier.Type), effect.modifier.type)));
        this.OnModifierTypeSet(this.modifierTypeDropdown);

        this.modifierTriggerDropdown.RefreshShownValue();
        this.modifierTypeDropdown.RefreshShownValue();
    }

    private void PopulateInputValues(Modifier modifier) {
        ActiveInputGameObject();

        this.valueInputField.text = modifier.effectiveChange.ToString();
    }
    private void ShowErrorMessage(string message) {
        Debug.Log("Error message: " + message);
    }
}
