using System;
using System.Collections.Generic;
using System.Linq;
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

    public Text summaryText;

    private Effect currentEffect = new Effect();

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
                modifierTargetValueDropdown.options = DropdownCreator.ConvertStringArrayToOptions(options, "Select a permanent modifier");
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

                this.currentEffect.target.type = Effect.Target.Type.TARGET_ATTRIBUTE;
                break;
            case Effect.Target.Type.TARGET_MONEY_GAIN:

            default:
                break;
        }

        this.currentEffect.target.type = targetType;
        this.RenderSummaryOfEffect(this.currentEffect);
    }

    private void PopulateSelectWithEnumValues<T>(MultiSelectController controller, int identifier) {
        List<MultiSelectController.Option> options = new List<MultiSelectController.Option>();

        List<T> attributes = Utils.GetEnumValues<T>();
        attributes.ForEach(modifier => {
            MultiSelectController.Option option = new MultiSelectController.Option((int)(object)modifier, modifier.ToString());

            options.Add(option);
        });
        controller.OnStartupMultiselect(options, identifier);
    }

    private void OnMultiSelectChange(int value, int identifier, MultiSelectController controller) {
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
                }
                break;
            default:
                throw new Exception("Unknown identifier " + identifier + " found in the onMultiSelectChange function");
        }

        this.RenderSummaryOfEffect(this.currentEffect);
    }

    public void onInputValueChanged(string inputIdentifier) {
        if (this.absoluteValueChangeText.text == "" && this.relativeValueChangeText.text == "") {
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
        }
        catch (FormatException e) {
            Debug.Log(e);
        }
    }

    private void AssignValueToTargetArguments(int absoluteValue = 0, int relativeValue = 0) {

    }

    private bool EffectHasTargetArguments(Effect effect) {
        return effect.target.arguments != null;
    }

    private void RenderSummaryOfEffect(Effect effect) {
        this.summaryText.text = Summarizer.SummarizeEffect(effect, true);
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
