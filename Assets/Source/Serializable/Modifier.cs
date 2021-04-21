using System;
using System.Collections.Generic;

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
    public enum PassiveValue
    {
        MOOD_GAIN,
        STRESS_GAIN,
        ENERG_GAIN
    }

    public Type type;

    public List<int> modifierTargets = new List<int>();
    public float effectiveChange;
}
