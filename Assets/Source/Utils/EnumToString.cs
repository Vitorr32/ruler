using System;

public class EnumToString
{
    public static string getStringValueOfEnum(Type enumType, object value) {
        return Enum.GetName(enumType, value);
    }

    public static string getStringOfModifiersEnum(Modifier.Type type = Modifier.Type.UNDEFINED) {
        switch (type) {
            case Modifier.Type.MODIFY_ATTRIBUTE_VALUE:
                return "Modify Attribute by absolute value";
            default:
                throw new System.Exception("Unknown modifier type: " + type);
        }
    }

    public static string GetStringOfConditionNumericSelectorValues(Condition.NumericSelector numericSelector, int firstValue, int? secondValue = null) {
        switch (numericSelector) {
            case Condition.NumericSelector.BETWEEN:
                return "value is between " + (firstValue == -1 ? 'X' : firstValue) + " and " + (secondValue == -1 ? 'Y' : secondValue);
            case Condition.NumericSelector.BIGGER_THAN:
                return "value is bigger than " + (firstValue == -1 ? 'X' : firstValue);
            case Condition.NumericSelector.SMALLER_THAN:
                return "value is smaller than " + (firstValue == -1 ? 'X' : firstValue);
            case Condition.NumericSelector.EXACTLY:
                return "value is exactly " + (firstValue == -1 ? 'X' : firstValue);
            default:
                return "";
        }
    }
}
