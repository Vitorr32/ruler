using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Trait
{
    public enum Type{
        UNDEFINED,
        NORMAL,
        PHYSICAL,
        MENTAL,
        PERSONALITY,
        SEXUAL,
        SPECIAL,

        MAX_TYPES
    }

    public int id;
    public string name;
    public string description;
    public int[] effects;

    public string spriteName;
    public Sprite sprite;

    public List<Effect> uEffects = new List<Effect>();
}
