using System;
using System.Collections.Generic;
using System.Linq;

public enum Condition
{
    THIS_CHARACTER_IS,
    TARGET_CHARACTER_IS,
    THIS_CHARACTER_HAS,
    TARGET_CHARACTER_HAS,
    TIME_IS,
    INTERACTING_WITH
}

public enum CharacterState
{
    TALKING,
    DEAD,
    AT
}

public enum CharacterHas
{
    RELATIONSHIP,
    AGE,
    TRAIT,
    ATTRIBUTE_VALUE,
    SKILL_VALUE,
    EVENT_FLAG
}

public enum CustomSelector
{
    CHARACTER,
    EVENT,
    LOCATION,
    TRAIT
}

public enum NumberInput
{
    BETWEEN,
    AT_LEAST,
    AT_MOST,
    IS
}
public static class ConditionMapper
{
    public class StepMapper
    {
        bool maximumSetter;
        bool minimumSetter;
        bool valueInput;

        bool customSelector;
        CustomSelector customSelectorType;
    }

    public static Dictionary<int, List<int>> triggerToTargetDictionary = new Dictionary<int, List<int>>() {
        {
            (int)Condition.THIS_CHARACTER_IS,
            ConditionMapper.EnumToIntArray<CharacterState>()
        },
        {
            (int)Condition.THIS_CHARACTER_HAS,
            ConditionMapper.EnumToIntArray<CharacterHas>()
        }
    };

    public static StepMapper getNextStepOfCondition(List<int> currentArgument) {
        switch ((Condition)currentArgument[1]) {
            case Condition.THIS_CHARACTER_HAS:
                return new StepMapper();
            default:
                return new StepMapper();
        }
    }

    public static List<int> EnumToIntArray<T>() {
        return ((int[])Enum.GetValues(typeof(T))).ToList();
    }

}
