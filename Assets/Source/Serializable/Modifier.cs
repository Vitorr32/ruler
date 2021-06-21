using System;
using System.Collections.Generic;

[Serializable]
public class Modifier
{
    public enum Type
    {
        UNDEFINED,

        MODIFY_ATTRIBUTE_VALUE,
        MODIFY_POTENTIAL_VALUE,
        MODIFY_RELATIONSHIP_VALUE,
        MODIFY_PASSIVE_ABSOLUTE_VALUE,
        MODIFY_PASSIVE_RELATIVE_VALUE,

        MAX_MODIFIERS
    }
    public enum PassiveValue
    {
        UNDEFINED,

        MOOD_GAIN,
        STRESS_GAIN,
        ENERGY_GAIN,
        POSITIVE_INTERACTION_CHANGE,
        NEGATIVE_INTERACTION_CHANGE,

        MAX_MODIFIERS
    }

    public Type type;

    public List<int> modifierTargets = new List<int>();
    public float effectiveChange;
    public bool targetSelf = true;
}
