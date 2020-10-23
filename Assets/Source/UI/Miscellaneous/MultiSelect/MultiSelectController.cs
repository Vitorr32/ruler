using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class MultiSelectController : MonoBehaviour
{
    private List<MultiSelectOption> selectOptions = new List<MultiSelectOption>();

    //Game object references
    public Dropdown dropdown;
    public Text multiselectLabel;

    public string label;

    void Start() {
        this.multiselectLabel.text = label;
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnDisable() {
        this.selectOptions.ForEach(option => {
            if (option.gameObject.activeSelf) {
                option.OnDisable();
            }
        });

        this.selectOptions = new List<MultiSelectOption>();
        this.dropdown.ClearOptions();
    }

    public void OnStartupMultiselect<T>(List<T> multiselectOptions, string valueParameter, string labelParameter) {
        int i = 0;
        foreach (T option in multiselectOptions) {
            int value = (int)typeof(T).GetProperty(valueParameter).GetValue(option);
            string label = (string)typeof(T).GetProperty(labelParameter).GetValue(option);

            i++;
        }
    }

    public void OnSelectionRemoval() {

    }
}
