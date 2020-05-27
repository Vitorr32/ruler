using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OfficerController
{
    public Officer baseOfficer;// new Officer(1, "Yolo", "von Nigga", System.DateTime.Now, null, new List<int> { 1, 2 });

    public List<Trait> serializedTraits;

    public bool onOverworld;

    public void StartUpController(Officer officer) {
        baseOfficer = officer;
    }
}
