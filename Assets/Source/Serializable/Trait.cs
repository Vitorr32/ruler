using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Trait
{
    public int id;
    public string name;
    public string description;
    public int[] effects;

    [NonSerialized]
    public List<Effect> uEffects = new List<Effect>();
}
