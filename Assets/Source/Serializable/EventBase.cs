using System;
using System.Collections.Generic;

[Serializable]
public class EventBase
{
    public int id;
    public string eventName;

    public struct Trigger
    {

    }

    public struct Flag
    {
        // Flag Name pattern : Context_Name_#FlagNumber
        public int flagId;
        public string flagName;

        //Whetever the flag has a time to expire or will never expire/ experie manually by another event
        public bool permanent;

        //How many minutes after the trigger the flag will last
        public int? minutesToExpire;

    }

    public Trigger trigger;
    public List<Flag> flags;
    public List<Scene> scenes;
    public List<Condition> conditions;
}
