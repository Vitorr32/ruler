using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ModifierEditor : MonoBehaviour
{
    public delegate void OnModiferEditorEnd(Effect effect);
    public static event OnModiferEditorEnd OnModifierEditorEnded;

    public Dropdown modifierTriggerDropdown;
    public GameObject modifierTargetGameObject;
    public Dropdown modifierTargetValueDropdown;

    public MultiSelectController primaryTargetSelectController;
    public MultiSelectController secondaryTargetSelectController;

    public GameObject valueChangeGO;
    public GameObject absoluteValueChangeGO;
    public Text absoluteValueChangeText;
    public GameObject relativeValueChangeGO;
    public Text relativeValueChangeText;

    public Text summaryText;

    private Effect currentEffect = new Effect();
    //private ModifierConfiguration modifierConfiguration;

    //public struct ModifierConfiguration
    //{
    //    public struct TargetConfiguration
    //    {
    //        public bool allowTarget;
    //        public Type targetType;
    //        public List<int> targetOptions;
    //    }

    //    public TargetConfiguration primaryTargetConfig;
    //    public TargetConfiguration secondaryTargetConfig;
    //    public ActionType actionType;
    //}

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
            this.gameObject.SetActive(true);
        }
    }

    public void OnModiferTypeChanged(Dropdown dropdown) {
        string[] options;

        modifierTargetGameObject.SetActive(true);
        Enum.TryParse(dropdown.options[dropdown.value].text, out Effect.Trigger.Type actionType);

        switch (actionType) {
            case Effect.Trigger.Type.ALWAYS_ACTIVE:

                //this.modifierConfiguration = new ModifierConfiguration() {
                //    actionType = actionType,
                //    primaryTargetConfig = new ModifierConfiguration.TargetConfiguration() {
                //        allowTarget = true,
                //        targetOptions = new List<int> {
                //            (int)Effect.Target.Type.TARGET_ATTRIBUTE,
                //            (int)Effect.Target.Type.TARGET_POPULARITY_GAIN,
                //            (int)Effect.Target.Type.TARGET_STRESS_GAIN
                //        },
                //        targetType = typeof(Effect.Target.Type)
                //    },
                //    secondaryTargetConfig = new ModifierConfiguration.TargetConfiguration() {
                //        allowTarget = false,
                //        targetOptions = null,
                //        targetType = null
                //    }
                //};

                options = new string[] {
                    Effect.Target.Type.TARGET_ATTRIBUTE.ToString(),
                    Effect.Target.Type.TARGET_POPULARITY_GAIN.ToString(),
                    Effect.Target.Type.TARGET_STRESS_GAIN.ToString()
                };
                modifierTargetValueDropdown.options = DropdownCreator.ConvertStringArrayToOptions(options, "Select a permanent modifier");
                break;
            case Effect.Trigger.Type.ON_INTERACTION:
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

        this.currentEffect.trigger.type = actionType;
        this.RenderSummaryOfEffect(this.currentEffect);
    }

    public void OnModifierTargetSet(Dropdown dropdown) {

        Enum.TryParse(dropdown.options[dropdown.value].text, out Effect.Target.Type targetType);
        switch (targetType) {
            case Effect.Target.Type.TARGET_ATTRIBUTE:
                PopulateSelectWithEnumValues<Officer.Attribute>(this.primaryTargetSelectController, (int)Effect.Target.Type.TARGET_ATTRIBUTE);
                ActivateAbsoluteChangeInput();
                ActivateRelativeChangeInput();

                break;
            case Effect.Target.Type.TARGET_MONEY_GAIN:

            default:
                break;
        }

        this.currentEffect.target.type = targetType;
        this.RenderSummaryOfEffect(this.currentEffect);
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

    private void OnMultiselectEventReceived(bool isAdding, int value, int identifier, MultiSelectController controller) {
        if (isAdding) {
            this.OnMultiSelectOptionAdded(value, identifier, controller);
        }
        else {
            this.OnMultiselectOptionRemoved(value, identifier, controller);
        }
    }

    private void OnMultiSelectOptionAdded(int value, int identifier, MultiSelectController controller) {
        if (controller != primaryTargetSelectController && controller != secondaryTargetSelectController) {
            return;
        }

        bool primaryTarget = controller == primaryTargetSelectController;
        switch ((Effect.Target.Type)identifier) {
            case Effect.Target.Type.TARGET_ATTRIBUTE:
                if (primaryTarget) {
                    //Check if the effect already has arguments for the Target varible
                    if (EffectHasTargetArguments(this.currentEffect)) {
                        int index = Array.FindIndex(this.currentEffect.target.arguments, argumentList => argumentList[0] == value);
                        //Only modify when the argument for that target don't exist yet
                        if (index == -1) {
                            this.currentEffect.target.arguments = this.currentEffect.target.arguments.Append(new int[] { value, 0, 0 }).ToArray();
                        }
                    }
                    //Initialize a new argument array
                    else {
                        this.currentEffect.target.arguments = new int[][] { new int[] { value, 0, 0 } };
                    }

                    this.ActivateAbsoluteChangeInput();
                    this.ActivateRelativeChangeInput();
                }
                break;
            default:
                throw new Exception("Unknown identifier " + identifier + " found in the onMultiSelectChange function");
        }

        this.RenderSummaryOfEffect(this.currentEffect);
    }

    private void OnMultiselectOptionRemoved(int value, int identifier, MultiSelectController controller) {
        if (controller != primaryTargetSelectController && controller != secondaryTargetSelectController) {
            return;
        }

        bool primaryTarget = controller == primaryTargetSelectController;
        switch ((Effect.Target.Type)identifier) {
            case Effect.Target.Type.TARGET_ATTRIBUTE:
                if (primaryTarget) {
                    int index = Array.FindIndex(this.currentEffect.target.arguments, argumentList => argumentList[0] == value);

                    if (index != -1) {
                        List<int[]> newArguments = this.currentEffect.target.arguments.ToList();
                        newArguments.RemoveAt(index);

                        this.currentEffect.target.arguments = newArguments.ToArray();
                    }
                    else {
                        throw new Exception("Tried to remove a target attribute list with value " + value + " but there was none");
                    }

                    if (this.currentEffect.target.arguments.Length == 0) {
                        this.DeactivateModifierValue();
                    }
                }
                break;
            default:
                throw new Exception("Unknown identifier " + identifier + " found in the OnMultiselectOptionRemoved function");
        }

        this.RenderSummaryOfEffect(this.currentEffect);
    }

    public void OnSubmitEffectToTrait() {

        if (CheckIfTargetArgumentsAreValid(this.currentEffect.target)) {
            this.currentEffect.id = PlayerPrefs.GetInt("effectIdCounter") + 1;
            PlayerPrefs.SetInt("effectIdCounter", this.currentEffect.id);
            OnModifierEditorEnded?.Invoke(this.currentEffect);
            this.CleanUpEditor();

            this.gameObject.GetComponent<PopupWrapper>().HidePopup();
        }
    }

    private bool CheckIfTargetArgumentsAreValid(Effect.Target target) {
        switch (target.type) {
            case Effect.Target.Type.TARGET_ATTRIBUTE:
                foreach (int[] argumentList in target.arguments) {
                    if (argumentList[1] != 0 && argumentList[2] != 0) {
                        this.ShowErrorMessage("A attribute can't be modified by an absolute and relative value at the same time");
                        return false;
                    }
                }

                return true;
        }

        return false;
    }

    public void OnInputValueChanged(string inputIdentifier) {
        if ((this.absoluteValueChangeText.text == "" && this.relativeValueChangeText.text == "") ||
            (this.currentEffect.target.arguments == null || this.currentEffect.target.arguments.Length == 0)) {
            return;
        }

        try {
            switch (inputIdentifier) {
                case "absoluteChange":
                    this.AssignValueToTargetArguments(Int32.Parse(absoluteValueChangeText.text));
                    break;
                case "relativeChange":
                    this.AssignValueToTargetArguments(0, Int32.Parse(relativeValueChangeText.text));
                    break;
            }

            this.RenderSummaryOfEffect(this.currentEffect);
        }
        catch (FormatException e) {
            Debug.Log(e);
        }
    }
    private void AssignValueToTargetArguments(int absoluteValue = 0, int relativeValue = 0) {
        for (int i = 0; i < this.currentEffect.target.arguments.Length; i++) {
            this.currentEffect.target.arguments[i][1] = absoluteValue;
            this.currentEffect.target.arguments[i][2] = relativeValue;
        }
    }
    private bool EffectHasTargetArguments(Effect effect) {
        return effect.target.arguments != null;
    }

    private void RenderSummaryOfEffect(Effect effect) {
        this.summaryText.text = Summarizer.SummarizeEffect(effect, true);
    }

    private void ActivateRelativeChangeInput() {
        this.valueChangeGO.SetActive(true);
        this.relativeValueChangeGO.SetActive(true);
        this.relativeValueChangeText.text = "";
    }
    private void ActivateAbsoluteChangeInput() {
        this.valueChangeGO.SetActive(true);
        this.absoluteValueChangeGO.SetActive(true);
        this.absoluteValueChangeText.text = "";
    }
    private void DeactivateModifierValue() {
        valueChangeGO.SetActive(false);
        this.relativeValueChangeGO.SetActive(false);
        this.absoluteValueChangeGO.SetActive(false);
    }
    private void CleanUpEditor() {
        this.DeactivateModifierValue();

        this.modifierTargetGameObject.SetActive(false);
        this.primaryTargetSelectController.gameObject.SetActive(false);
        this.secondaryTargetSelectController.gameObject.SetActive(false);
        this.summaryText.text = "";
        this.absoluteValueChangeText.text = "";
        this.relativeValueChangeText.text = "";

        this.modifierTriggerDropdown.GetComponent<DropdownCreator>().ResetDropdownState();
        this.modifierTargetValueDropdown.GetComponent<DropdownCreator>().ResetDropdownState();

        this.currentEffect = new Effect();
    }
    private void PopulateEffectValuesForEdit(Effect effect) {
        this.currentEffect = effect;

        this.modifierTriggerDropdown.SetValueWithoutNotify(this.modifierTriggerDropdown.options.FindIndex(option => option.text == Enum.GetName(typeof(Effect.Trigger.Type), effect.trigger.type)));
        this.OnModiferTypeChanged(this.modifierTriggerDropdown);
        this.modifierTargetValueDropdown.SetValueWithoutNotify(this.modifierTargetValueDropdown.options.FindIndex(option => option.text == Enum.GetName(typeof(Effect.Target.Type), effect.target.type)));
        this.OnModifierTargetSet(this.modifierTargetValueDropdown);

        this.modifierTriggerDropdown.RefreshShownValue();
        this.modifierTargetValueDropdown.RefreshShownValue();
        this.PopulatePrimaryTargetValues(this.currentEffect.target);


        this.RenderSummaryOfEffect(this.currentEffect);
    }

    private void PopulatePrimaryTargetValues(Effect.Target target) {
        switch (target.type) {
            case Effect.Target.Type.TARGET_ATTRIBUTE:
                PopulateSelectWithEnumValues<Officer.Attribute>(
                    this.primaryTargetSelectController,
                    (int)Effect.Target.Type.TARGET_ATTRIBUTE,
                    this.currentEffect.target.arguments.Select(argumentList => argumentList[0]).ToArray()
                );

                this.primaryTargetSelectController.gameObject.SetActive(true);
                break;
        }
    }

    private void PopulateInputValues(Effect.Target target) {
        switch (target.type) {
            default:
                this.absoluteValueChangeText.text = target.arguments[1].ToString();
                this.relativeValueChangeText.text = target.arguments[2].ToString();

                this.ActivateAbsoluteChangeInput();
                this.ActivateRelativeChangeInput();
                break;
        }
    }
    private void ShowErrorMessage(string message) {
        Debug.Log("Error message: " + message);
    }
}
