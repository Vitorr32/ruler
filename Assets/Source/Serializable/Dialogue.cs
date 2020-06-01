using System;
using System.Collections;
using System.Collections.Generic;
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
    public class TraitPush
    {
        int traitID;
        int weight;
    }

    public List<TraitPush> traitPushes;
    public DialogueType type;
    //Modifier that this dialogue suffers when taking into account the officer mood from Happy (100) to Unhappy(0)
    public float moodModifier;
    public string text;
}
