
using System;
using System.Collections.Generic;

[Serializable]
public class Condition
{
    public enum LogicOperator
    {
        UNDEFINED,

        IF,
        AND,
        OR,

        MAX_LOGIC_OPERATORS
    }

    public LogicOperator logicOperator = LogicOperator.UNDEFINED;

    public enum Initiator
    {
        UNDEFINED,

        STATUS_RANGE,
        ATTRIBUTE_RANGE,
        TRAIT,
        EVENT_FLAGGED,
        LOCATION,
        TIME,
        RELATIONSHIP,

        MAX_TYPES
    }

    public Initiator initiator = Initiator.UNDEFINED;

    public enum Agent
    {
        UNDEFINED,

        SELF,
        TARGET,
        SPECIFIC,
        SELF_TARGET,
        SELF_SPECIFIC,
        SPECIFIC_SPECIFIC,
        GLOBAL,

        MAX_SPECIFICATORS
    }

    public enum NumericSelector
    {
        UNDEFINED,

        BIGGER_THAN,
        SMALLER_THAN,
        BIGGER_THAN_SELF,
        BIGGER_THAN_TARGET,
        BETWEEN,
        EXACTLY,

        MAX_NUMERIC_SELECTORS
    }

    public Agent agent = Agent.UNDEFINED;

    public struct StatusRange
    {
        public NumericSelector selector;

        // Up to 3 parameters [Status Enumerator, First Input, Second Input]
        public int[] statusRangeParameters;
    }

    public StatusRange statusRange;

    public struct AttributeRange
    {
        public NumericSelector selector;

        // Up to 3 parameters [Skill ID, First Input, Second Input]
        public int[] attrRangeParameters;
    }

    public AttributeRange attrRange;

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
        public DateTime timestamp;
    }

    public Time time;

    public struct Relationship
    {
        public enum Selector
        {
            UNDEFINED,

            STATUS,
            KNOWLEDGE,

            MAX_SELECTORS
        }

        public enum SelectableStatus
        {
            UNDEFINED,

            MAX_STATUS
        }

        public Selector selector;
        public SelectableStatus status;
    }

    public Relationship relationship;

    public bool EvaluateCondition(Character self, List<Character> targets, int? world = null) {
        return true;
    }
}
