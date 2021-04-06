using System;

[Serializable]
public class Character
{
    public enum Kinship
    {
        FATHER,
        MOTHER,
        SIBLING,
        CHILD
    }

    public struct KinshipStruct
    {
        public int character_id;
        public Kinship kinship;
    }

    public int id;
    public string[] sprites;
    public string name;
    public string surname;
    public int age;
    public DateTime birthday;

    public int mood;
    public int stress;
    public int energy;

    public KinshipStruct[] family;
    public Trait[] traits;
    public Event.Flag[] flags;
    public Skill[] skills;

    public void BuildBasicSkillTree() {
        
    }
}
