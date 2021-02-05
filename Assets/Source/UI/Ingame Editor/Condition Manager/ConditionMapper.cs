using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public enum LogicOperator
{
    IF,
    AND,
    OR
}
public enum Condition
{
    UNDEFINED,
    THIS_CHARACTER_IS,
    TARGET_CHARACTER_IS,
    THIS_CHARACTER_HAS,
    TARGET_CHARACTER_HAS,
    TIME_IS,
    INTERACTING_WITH,

    MAX_CONDITIONS
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


public struct ConditionStruct
{
    public enum Type
    {
        UNDEFINED,

        ATTRIBUTE_RANGE,
        TRAIT,
        EVENT_FLAGGED,
        LOCATION,
        TIME,

        MAX_TYPES
    }

    public enum Specificator
    {
        UNDEFINED,

        SELF,
        TARGET,
        SPECIFIC,

        MAX_SPECIFICATORS
    }

    public Type type;
    public Specificator specificator;

    public struct AttributeRange
    {
        public enum Selector
        {
            UNDEFINED,

            BIGGER_THAN,
            SMALLER_THAN,
            BETWEEN,
            EXACTLY,

            MAX_SELECTORS
        }
        public enum SelectableAttribute
        {
            ATTRIBUTES,
            SKILL,
            TITLE,
            STATUS
        }

        public Selector selector;
        public SelectableAttribute selectableAttribute;

        //Which Attribute/Skill/Title/Status enum/id was selected, should be converted to an int for easy storage on JSON
        public int selectedAttributeValue;
        // Selector BETWEEN has 2 parameters (min and max), the others have only one
        public int[] attrRangeParameters;
    }

    public AttributeRange attributeRange;

    public struct Trait
    {
        public enum Selector
        {
            UNDEFINED,

            HAS,
            DONT,

            MAX_SELECTORS
        }

        public Selector selector;

        //Which trait was specified in this condition
        public int selectedTraitID;
    }

    public Trait trait;

    public struct EventFlagged
    {
        public enum Selector
        {
            UNDEFINED,

            TRIGGERED,
            NOT_TRIGGERED,

            MAX_SELECTORS
        }

        public Selector selector;
        //Which event flag was specified
        public int selectedEventFlagID;
    }

    public EventFlagged eventFlagged;

    public struct Location
    {
        public enum Selector
        {
            UNDEFINED,

            IS_AT,
            IS_NOT_AT,

            MAX_SELECTORS
        }

        public Selector selector;
        //Which location id was specified
        public int selectedLocationID;
    }

    public Location location;

    public struct Time
    {
        public enum Selector
        {
            UNDEFINED,

            EXACTLY,
            BEFORE,
            AFTER,

            MAX_SELECTORS
        }

        public Selector selector;
        //Which location id was specified
        public int timestamp;
    }
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
            case Condition.THIS_CHARACTER_HAS:
                //currentStepMapper.customSelector = true;
                //currentStepMapper.customSelectorType = CustomSelector.CHARACTER
                return new StepsMapper() { maximumSetter = true };
            default:
                return new StepsMapper();
        }
    }

    public static List<int> EnumToIntArray<T>() {
        return ((int[])Enum.GetValues(typeof(T))).ToList();
    }

}
