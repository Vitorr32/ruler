
using System;

[Serializable]
public class Condition
{
    public struct Struct
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
}
