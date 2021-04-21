﻿using System;

public class EnumToString
{
    public static string getStringValueOfEnum(Type enumType, object value) {
        return Enum.GetName(enumType, value);
    }

    public static string getStringOfModifiersEnum(Modifier.Type type = Modifier.Type.UNDEFINED) {
        switch (type) {
            case Modifier.Type.MODIFY_SKILL_VALUE:
                return "Modify Attribute by absolute value";
            default:
                throw new System.Exception("Unknown modifier type: " + type);
        }
    }

}
