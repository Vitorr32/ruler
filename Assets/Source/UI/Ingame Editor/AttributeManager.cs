using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributeManager : MonoBehaviour
{
    public Text remaningPointsText;
    public Dropdown tierDropdown;

    private int maxPointsValue;

    // Start is called before the first frame update
    void Start() {
        tierDropdown.onValueChanged.AddListener(delegate {
            OnTierBeenChoosen(tierDropdown);
        });
    }

    // Update is called once per frame
    void Update() {

    }

    public void OnTierBeenChoosen(Dropdown tierDropdown) {
        Officer.Tier tier = (Officer.Tier)Enum.Parse(typeof(Officer.Tier), tierDropdown.options[tierDropdown.value].text);

        maxPointsValue = (int)tier;

        remaningPointsText.text = maxPointsValue.ToString();
    }
}
