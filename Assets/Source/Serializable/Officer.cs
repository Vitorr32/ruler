using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Officer
{
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

        MAX_RACES
    }

    public enum Skill
    {
        STAMINA,
        STRENGTH
    }

    public enum Faith
    {
        UNDEFINED,
        FAITHLESS,
        ATHEIST,
        CHRISTIAN,
        JEWISH,
        ORTHODOX,
        MUSLIM,
        BUDHIST,

        MAX_FAITHS
    }

    public enum Status
    {
        UNDEFINED,
        SLAVE,
        INDENTURE_SLAVE,
        SERF,
        FREE_MAN,
        WEALTHY_CITIZEN,
        MINOR_NOBLE,
        NOBLE,

        MAX_STATUS
    }

    public enum Attribute
    {
        UNDEFINED,

        DIPLOMACY,
        MARTIAL,
        INTELIGENCE,

        MAX_ATTRIBUTES
    }


    public enum Tier
    {
        UNDEFINED,
        NOBODY = 100,
        LOCAL_FAME = 125,
        REGIONAL_RENOWN = 150,
        CONTINENTAL_POWERHOUSE = 200,
        WORLD_UNIFIER = 300,

        MAX_ATTRIBUTES = 6
    }

    public struct Hobby
    {
        public enum Type
        {
            UNDEFINED,
            HUNTING,
            FALCONRY,
            MELEE,
            LITERATURE,
            GAMES,
            DANCE,
            MUSIC,
            FESTIVALS,
            LANGUAGES,
            FISHING,

            MAX_TYPES
        }

        public bool isKnownByPlayer;
        public int passion;
        public Type type;
    }

    public Officer(int pID, string pFirstName, string pFamilytName, string pBirth, string pDeath, List<int> pTraits, int[] pPosition) {
        id = pID;
        firstName = pFirstName;
        familyName = pFamilytName;
        birth = pBirth;
        death = pDeath;
        traits = pTraits;
        position = pPosition;
    }

    public int id;
    public string firstName;
    public string familyName;
    public string birth;
    public string death;

    public Gender gender;
    public Race race;
    public Faith faith;
    public int diplomacy;
    public int martial;
    public int inteligence;
    //Mood is the emotional state of the officer, from 100 (Very Happy) to 0 (Very Unhappy)
    public int mood = 50;

    public List<int> traits;
    public List<string> spriteNames = new List<string>() { "default_child", "default_child", "default_child" };
    public List<RelationshipTemp> relationships;
    public int[] position;
}
