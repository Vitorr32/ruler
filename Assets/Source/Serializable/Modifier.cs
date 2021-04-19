using System;

[Serializable]
public class Modifier
{
    public enum Type
    {
        UNDEFINED,

        MODIFY_SKILL_VALUE,
        MODIFY_SKILL_POTENTIAL_VALUE,
        MODIFY_PASSIVE_ABSOLUTE_VALUE,
        MODIFY_PASSIVE_RELATIVE_VALUE,

        MAX_MODIFIERS
    }

    public Type type;
    public int[] arguments;
}
