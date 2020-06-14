using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Officer
{
    public enum Gender
    {
        MALE,
        FEMALE
    }
    public enum Race
    {
        HUMAN,
        ORC,
        ELF,
        DWARF
    }

    public enum Faith
    {
        FAITHLESS,
        ATHEIST,
        CHRISTIAN,
        JEWISH,
        ORTHODOX,
        MUSLIM,
        BUDHIST
    }

    public enum Status
    {
        SLAVE,
        INDENTURE_SLAVE,
        SERF,
        FREE_MAN,
        WEALTHY_CITIZEN,
        MINOR_NOBLE,
        NOBLE
    }

    public struct Hobby
    {
        public enum Type
        {
            HUNTING,
            FALCONRY,
            MELEE,
            LITERATURE,
            GAMES,
            DANCE,
            MUSIC,
            FESTIVALS,
            LANGUAGES,
            FISHING
        }

        public bool isKnownByPlayer;
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
    public List<string> sprites = new List<string>() { "default_child", "default_child", "default_child" };
    public List<Relationship> relationships;
    public int[] position;
}
