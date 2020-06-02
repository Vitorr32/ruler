using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public struct Restriction
{
    public enum Type
    {
        HAS_TRAIT,
        ATTRIBUTE_HIGHER_THAN,
        ATTRIBUTE_LOWER_THAN,
        MOOD_HIGHER_THAN,
        MOOD_LOWER_THAN,
        SKILL_HIGHER_THAN
    }

    public Type type;
    public int[] arguments;
}
public enum DialogueType
{
    INTRODUCTION,
    SMALL_TALK,
    LONG_TALK,
    DEEP_TALK,
    KNOW_MORE,
    DISCUSS_HOOBIES,
    DISCUSS_CHARACTER,
    DISCUSS_SKILLS,
    DISCUSS_RELIGION,
    DISCUSS_RACE
}
[Serializable]
public class Dialogue
{
    [Serializable]
    //How much a specific trait has a tendency of chosing this specific dialogue
    public struct TraitPush
    {
        public int traitID;
        public float weight;
    }

    public List<TraitPush> traitPushes;
    public List<Restriction> restrictions;
    public DialogueType type;
    //Modifier that this dialogue suffers when taking into account the officer mood from Happy (100) to Unhappy(0)
    public float moodModifier = 1;
    public int weight = 1; //How strong is the tendecy for this dialogue be choosen over the rest, the default is 1
    public string text;
}
