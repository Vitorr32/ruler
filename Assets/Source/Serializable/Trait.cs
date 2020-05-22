using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trait
{
    Trait(int pID, Type pType, Effect[] pEffects, Trait[] pProguession) {
        id = pID;
        type = pType;
        effects = pEffects;
        proguession = pProguession;
    }

    public enum Type
    {
        UNKNOWN_TYPE,
        VALOROUS,
        SKILLED_HUNTER
    }

    public int id;
    public Type type;
    public Effect[] effects;

    public Trait[] proguession;

}
