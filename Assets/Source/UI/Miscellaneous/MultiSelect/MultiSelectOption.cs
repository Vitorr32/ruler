using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiSelectOption : MonoBehaviour
{
    private MultiSelectController controller;
    private MultiSelectController.Option option;

    public Text optionLabel;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void ActivateOption(MultiSelectController.Option option, MultiSelectController controller) {
        this.gameObject.SetActive(true);
        this.option = option;
        this.controller = controller;
        this.optionLabel.text = this.option.label;
    }

    public void DisableOption() {
        this.option = null;

        this.gameObject.SetActive(false);
        this.gameObject.GetComponentInChildren<Text>().text = "";
    }
    public void OnRemoveOptionButtonClick() {
        this.controller.OnSelectionRemoval(this.option);
        this.DisableOption();
    }
}
