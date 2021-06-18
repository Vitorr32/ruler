using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Conversation
{
    public enum Type
    {
        INTRODUCTION,
        SMALL_TALK,
        LONG_TALK,
        DEEP_TALK,
        KNOW_MORE,
        DISCUSS_HOOBIES,
        DISCUSS_CHARACTER,
        UNIQUE_CONVERSATION
    }

    public string id;
    //What will trigger this conversation
    public Type type;
    //ID of the Dialogue that should start the conversation
    public string rootDialogueID;
    //All the dialogues associated to this conversation
    public List<Dialogue> dialogues;
}
