using System;

[Serializable]
public class Effect
{
    [Serializable]
    public class Trigger
    {
        public ActionType type;
        public int[] arguments;
    }
    [Serializable]
    public struct Restriction
    {
        public enum Type
        {
            UNDEFINED,
            NO_RESTRICTION,
            AGAINST_GENDER,
            AGAINST_RACE,
            AGAINST_FAITH,
            NOT_AGAINST_GENDER,
            NOT_AGAINST_RACE,
            NOT_AGAINST_FAITH,

            MAX_RESTRICTIONS
        }

        public Type type;
        public int[] arguments;
    }
    [Serializable]
    public struct Modifier
    {
        public enum Type
        {
            UNDEFINED,
            MODIFY_ATTRIBUTE_ABSOLUTE,
            MODIFY_ATTRIBUTE_RELATIVE,

            MAX_MODIFIERS
        }

        public Type type;
        public int[] arguments;
    }
    [Serializable]
    public struct Duration
    {
        public enum Type
        {
            PERMANENT,
            ON_TURN_END,
            ON_TURN_START,
            SPECIFIC_DURATION,
            SPECIFIC_DATE
        }

        public Type type;
        public int[] arguments;
    }

    public int id;
    //Source is with item/trait/race is the source of the effect
    public int sourceID;
    public Trigger trigger;
    public Duration duration;
    public Restriction[] restrictions;
    public Modifier[] modifiers;
}
