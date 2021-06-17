using System;
using System.Collections.Generic;

[Serializable]
public class EventBase
{
    public int id;
    public string eventName;

    public struct Trigger
    {
        //Check if the event should activate
        Condition condition;
        //Get all the actors to participate in the event
        List<Condition> queryActorsConditions;
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
    //Flag that determine if event has visual scenes or should be an background event
    public bool isVisualEvent;
    //Time in minutes that takes to an event wich trigger is positive to happen
    public int meanTimeToHappen;
    public List<Scene> scenes;
    public List<Condition> conditions;
}
