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

    public List<int> traits;
    public int[] position;
}
