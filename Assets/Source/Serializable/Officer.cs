using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Officer
{
    public Officer(int pID, string pFirstName, string pFamilytName, DateTime pBirth, DateTime? pDeath, List<int> pTraits) {
        id = pID;
        firstName = pFirstName;
        familyName = pFamilytName;
        birth = pBirth;
        death = pDeath;
        traits = pTraits;
    }

    public int id;
    public string firstName;
    public string familyName;
    public DateTime birth;
    public DateTime? death;
    public List<int> traits;

}
