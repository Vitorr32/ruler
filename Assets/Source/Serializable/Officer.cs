using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Officer
{
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
    public List<int> traits;
    public int[] position;
}
