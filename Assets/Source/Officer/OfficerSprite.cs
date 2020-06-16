using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[Serializable]
public class OfficerSprite
{
    public enum Age
    {
        UNDEFINED,
        TEEN,
        YOUNG,
        MIDDLE_AGE
    }

    public enum Gender
    {
        UNDEFINED,
        MALE,
        FEMALE
    }

    public string filename;
    public Sprite sprite;
    public Gender gender;
    public Age age;

    public OfficerSprite(Sprite rawSprite) {
        filename = rawSprite.name;
        sprite = rawSprite;

        GetValuesFromFilename(filename.Split(Constants.SPRITE_SEPARATOR));
    }

    private void GetValuesFromFilename(string[] filename) {
        string genderSubstring = filename[Constants.OFFICER_SPRITE_GENDER_INDEX];

        if (Constants.MALE.Contains(genderSubstring)) {
            gender = Gender.MALE;
        }
        else if (Constants.FEMALE.Contains(genderSubstring)) {
            gender = Gender.FEMALE;
        }
        else {
            gender = Gender.UNDEFINED;
        }

        string ageSubstring = filename[Constants.OFFICER_SPRITE_AGE_INDEX];

        if (Constants.TEEN.Contains(ageSubstring)) {
            age = Age.TEEN;
        }
        else if (Constants.YOUNG.Contains(ageSubstring)) {
            age = Age.YOUNG;
        }
        else if (Constants.MIDDLE_AGE.Contains(ageSubstring)) {
            age = Age.MIDDLE_AGE;
        }
        else {
            age = Age.UNDEFINED;
        }
    }
}
