using System;
using System.Collections.Generic;
using System.Linq;

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
    UNDEFINED,

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
    public struct StepsMapper
    {
        public LogicOperator logicOperator;
        public Condition conditionInitiator;

        public bool maximumSetter;
        public bool minimumSetter;
        public bool valueInput;

        public bool customSelector;
        public CustomSelector customSelectorType;
    }

    public static StepsMapper getNextStepOfCondition(StepsMapper currentStepMapper) {
        switch (currentStepMapper.conditionInitiator) {
            //case Condition.THIS_CHARACTER_HAS:
            //    //currentStepMapper.customSelector = true;
            //    //currentStepMapper.customSelectorType = CustomSelector.CHARACTER
            //    return new StepsMapper() { maximumSetter = true };
            default:
                return new StepsMapper();
        }
    }

    public static List<int> EnumToIntArray<T>() {
        return ((int[])Enum.GetValues(typeof(T))).ToList();
    }

}
