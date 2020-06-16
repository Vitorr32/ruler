﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    //On the sprite name, which characters will be used to identify the atrtibutes of the image
    public const int OFFICER_SPRITE_GENDER_INDEX = 1;
    public const int OFFICER_SPRITE_AGE_INDEX = 2;
    public const int OFFICER_SPRITE_ID_INDEX = 3;

    public const char SPRITE_SEPARATOR = '_';

    public static readonly string[] MALE = { "M", "MALE" };
    public static readonly string[] FEMALE = { "F", "FEMALE" };

    public static readonly string[] TEEN = { "T", "TEEN" };
    public static readonly string[] YOUNG = { "Y", "YOUNG" };
    public static readonly string[] MIDDLE_AGE = { "M", "OLD", "MIDDLE-AGE" };
}
