using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attach this script to any gameobject to make sure it doesn't be destroy on scene transition
public class DDOL : MonoBehaviour
{    void Awake() {
        DontDestroyOnLoad(gameObject);
    }
}
