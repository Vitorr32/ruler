using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DropdownType
{
    UNDEFINED,

    OFFICER_GENDER,
    OFFICER_RACE,
    OFFICER_RELIGION,
    OFFICER_STATUS,
    OFFICER_TIER,
    TRAIT_TYPE,
    TRIGGERS,
    MODIFIER_TARGET,
    RESTRICTION_TYPE
}
public class DropdownCreator : MonoBehaviour
{
    public DropdownType type = DropdownType.UNDEFINED;
    public bool revealAll = false;

    private Dropdown dropdown;
    // Start is called before the first frame update
    void Start() {
        this.dropdown = GetComponent<Dropdown>();

        if (type != DropdownType.UNDEFINED) {
            this.dropdown.options = PopulateDropdown(type);
        }
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
                return ConvertStringArrayToOptions(Utils.GetEnumValues<Officer.Tier>());
            case DropdownType.TRAIT_TYPE:
                return ConvertStringArrayToOptions(Utils.GetEnumValues<Trait.Type>());
            case DropdownType.TRIGGERS:
                return ConvertStringArrayToOptions(Utils.GetEnumValues<ActionType>());
            case DropdownType.MODIFIER_TARGET:
                return ConvertStringArrayToOptions(Utils.GetEnumValues<Effect.Target.Type>());
            case DropdownType.RESTRICTION_TYPE:
                return ConvertStringArrayToOptions(Utils.GetEnumValues<Effect.Restriction.Type>(this.revealAll));
            default:
                Debug.LogError("The dropdown " + gameObject.name + " has no type set!");

                return null;
        }
    }

    public static List<Dropdown.OptionData> ConvertStringArrayToOptions(string[] enumNames) {
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();

        for (int i = 0; i < enumNames.Length; i++) {
            options.Add(new Dropdown.OptionData() { text = enumNames[i] });
        }

        return options;
    }

    public static List<Dropdown.OptionData> ConvertStringArrayToOptions<T>(List<T> enumNames) {
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();

        for (int i = 0; i < enumNames.Count; i++) {
            options.Add(new Dropdown.OptionData() { text = enumNames[i].ToString() }); ;
        }

        return options;
    }
}
