using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OfficerController : MonoBehaviour
{
    public Officer officer = new Officer(1, "Yolo", "von Nigga", System.DateTime.Now, null, new List<int> { 1, 2 });

    public List<Trait> serializedTraits;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
