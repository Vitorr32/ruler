using System;
using System.Collections.Generic;

[Serializable]
public class Character
{
    public enum Kinship
    {
        PARENT,
        SIBLING,
        CHILD
    }

    public struct KinshipStruct
    {
        public int character_id;
        public Kinship kinship;
    }

    public enum Gender
    {
        UNDEFINED,

        MALE,
        FEMALE,

        MAX_GENDERS
    }

    public enum Race
    {
        UNDEFINED,

        HUMAN,
        ORC,
        ELF,
        DWARF,

        MAX_ETHINICITIES
    }

    public enum Status
    {
        UNDEFINED,

        MOOD,
        STRESS,
        ENERGY,

        MAX_STATUS
    }

    //Absolute Basic values of the character, these will never change
    public int id;
    public string[] sprites;
    public string name;
    public string surname;
    public int age;
    public DateTime birthday;

    //Current state of the character attributes that should be serialized in case of save
    public int baseMood = 50;
    public int baseStress = 0;
    public int baseEnergy = 100;

    public Race race;
    public Gender gender;
    public KinshipStruct[] family;
    public Trait[] traits;
    public Event.Flag[] flags;
    public Attribute[] skills;
    public List<string> spriteNames = new List<string>() { "default_child", "default_teen", "default_adult" };

    public void BuildBasicSkillTree() {

    }
}
