public class EnumToString
{
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
}
