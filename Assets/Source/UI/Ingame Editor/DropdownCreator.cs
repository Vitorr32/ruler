using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DropdownType
{
    OFFICER_GENDER,
    OFFICER_RACE,
    OFFICER_RELIGION,
    OFFICER_STATUS,
    OFFICER_TIER
}
public class DropdownCreator : MonoBehaviour
{
    public DropdownType type;
    private Dropdown dropdown;
    // Start is called before the first frame update
    void Start() {
        dropdown = GetComponent<Dropdown>();

        dropdown.options = PopulateDropdown(type);
    }

    List<Dropdown.OptionData> PopulateDropdown(DropdownType type) {
        switch (type) {
            case DropdownType.OFFICER_GENDER:
                return ConvertStringArrayToOptions(System.Enum.GetNames(typeof(Officer.Gender)));
            case DropdownType.OFFICER_RACE:
                return ConvertStringArrayToOptions(System.Enum.GetNames(typeof(Officer.Race)));
            case DropdownType.OFFICER_RELIGION:
                return ConvertStringArrayToOptions(System.Enum.GetNames(typeof(Officer.Faith)));
            case DropdownType.OFFICER_STATUS:
                return ConvertStringArrayToOptions(System.Enum.GetNames(typeof(Officer.Status)));
            case DropdownType.OFFICER_TIER:
                return ConvertStringArrayToOptions(System.Enum.GetNames(typeof(Officer.Tier)));
            default:
                Debug.LogError("The dropdown " + gameObject.name + " has no type set!");

                return null;
        }
    }

    List<Dropdown.OptionData> ConvertStringArrayToOptions(string[] enumNames) {
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();

        for (int i = 0; i < enumNames.Length; i++) {
            options.Add(new Dropdown.OptionData() { text = enumNames[i] });
        }

        return options;
    }
}
