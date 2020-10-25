using System;

public class EnumToString
{
    public static string getStringValueOfEnum(Type enumType, object value) {
        return Enum.GetName(enumType, value);
    }

    public static string getStringOfModifiersEnum(Effect.Modifier.Type type = Effect.Modifier.Type.UNDEFINED) {
        switch (type) {
            case Effect.Modifier.Type.MODIFY_ATTRIBUTE_ABSOLUTE:
                return "Modify Attribute by absolute value";
            case Effect.Modifier.Type.MODIFY_ATTRIBUTE_RELATIVE:
                return "Modify attribute by relative value";
            default:
                throw new System.Exception("Unknown modifier type: " + type);
        }
    }

    public static string getStringOfRestrictionsEnum(Effect.Restriction.Type type = Effect.Restriction.Type.UNDEFINED) {

        return getStringValueOfEnum(typeof(Effect.Restriction.Type), Effect.Restriction.Type.AGAINST_FAITH);
        //switch (type) {
        //    case Effect.Restriction.Type.AGAINST_FAITH:
        //        return getStringValueOfEnum(typeof(Effect.Restriction.Type), Effect.Restriction.Type.AGAINST_FAITH);
        //    case Effect.Modifier.Type.MODIFY_ATTRIBUTE_RELATIVE:
        //        return "Modify attribute by relative value";
        //    default:
        //        throw new System.Exception("Unknown modifier type: " + type);
        //}
    }
}
