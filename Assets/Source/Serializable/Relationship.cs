using System;
using System.Collections.Generic;

[Serializable]
public class Relationship
{
    public int targetCharId;
    public int selfCharId;

    public struct Relation
    {
        public int respect;
        public int favorability;
        public int attraction;
        public int love;
        public int powerDynamic;
    }
    public Relation relation;

    public struct Knowledge
    {
        public int familiarity;
        public int status;
        public int skill;
    }
    public Knowledge knowledge;

    public List<int> relationEventFlag;
    public List<int> relationEvents;
    public DateTime establishedDate;
}
