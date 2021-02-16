using System;
using System.Collections.Generic;

[Serializable]
public class Event
{
    public int id;
    public string eventName;

    public struct Flag
    {
        // Flag Name pattern : Context_Name_#FlagNumber
        public int flagId;
        public string flagName;

        //Whetever the flag has a time to expire
        public bool permanent;

        //How many minutes after the trigger the flag will last
        public int? minutesToExpire;

    }

    public List<Flag> flags;
    public List<Scene> scenes;
    public List<Condition> conditions;
}
