using System;

public class EnumToString
{
    public static string getStringValueOfEnum(Type enumType, object value) {
        return Enum.GetName(enumType, value);
    }

    public static string getStringOfModifiersEnum(Effect.Modifier.Type type = Effect.Modifier.Type.UNDEFINED) {
        switch (type) {
            case Effect.Modifier.Type.MODIFY_ATTRIBUTE_VALUE:
                return "Modify Attribute by absolute value";
            default:
                throw new System.Exception("Unknown modifier type: " + type);
        }
    }

}
