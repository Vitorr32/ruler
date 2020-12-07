using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class MultiSelectController : MonoBehaviour
{
    public class Option
    {
        public int value;
        public string label;
        public bool selected;

        public Option(int value, string label) {
            this.value = value;
            this.label = label;
            this.selected = false;
        }
    }

    public delegate void OnMultiSelectChange(int value, int identifier, MultiSelectController controller);
    public static event OnMultiSelectChange onMultiselectChanged;

    private List<Option> selectOptions = new List<Option>();
    public List<MultiSelectOption> multiselectOptionPrefabPool = new List<MultiSelectOption>();

    //Game object references
    public Dropdown dropdown;
    public Text multiselectLabel;

    public string label;
    public int multiSelectIdentifier;

    void Start() {
        this.multiselectLabel.text = label;
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnDisable() {
        this.multiselectOptionPrefabPool.ForEach(option => {
            if (option.gameObject.activeSelf) {
                option.DisableOption();
            }
        });
        this.selectOptions = new List<Option>();
        this.dropdown.ClearOptions();
    }

    public void OnStartupMultiselect(List<Option> multiselectOptions, int identifier) {
        this.selectOptions = multiselectOptions;
        this.multiSelectIdentifier = identifier;

        this.gameObject.SetActive(true);
        this.PopulateDropdownWithValues(this.selectOptions);
    }

    public void OnDropdownSelection() {
        string selectedLabel = this.dropdown.options[this.dropdown.value].text;

        if (selectedLabel == "") {
            return;
        }

        Option selectedOption = this.selectOptions.Find(option => option.label == selectedLabel);
        selectedOption.selected = true;

        this.OnUpdateMultiselectList(selectOptions);
        this.dropdown.SetValueWithoutNotify(0);

        onMultiselectChanged?.Invoke(selectedOption.value, this.multiSelectIdentifier, this);
    }

    private void OnUpdateMultiselectList(List<Option> options) {
        List<Option> selectedOptions = GetSelectedOptions(options);

        if (this.selectOptions.Count > this.multiselectOptionPrefabPool.Count) {
            throw new System.Exception("Number of selected options in MultiSelect bigger than the prefab pool");
        }

        int i = 0;
        this.multiselectOptionPrefabPool.ForEach(prefab => {
            if (i < selectedOptions.Count) {
                prefab.ActivateOption(selectedOptions[i], this);
            }
            else {
                prefab.DisableOption();
            }
            i++;
        });
    }

    private List<Dropdown.OptionData> PopulateDropdownWithValues(List<Option> dropdownValues) {
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();

        options.Add(new Dropdown.OptionData() { text = "" });
        dropdownValues.ForEach(dropdownValue => {
            options.Add(new Dropdown.OptionData() { text = dropdownValue.label });
        });

        this.dropdown.options = options;

        return options;
    }

    private List<Option> GetSelectedOptions(List<Option> options) {
        return options.Where((option) => option.selected).ToList();
    }

    public void OnSelectionRemoval(Option option) {
        Option toDeleteOption = this.selectOptions.Find(iteratedOption => iteratedOption == option);

        toDeleteOption.selected = false;
    }
}
