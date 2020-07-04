using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorManager : MonoBehaviour
{
    public enum Tabs
    {
        UNDEFINED,
        OFFICER,
        TRAIT,
        EFFECT,
        DIALOGUE,

        MAX_TABS
    }

    public List<GameObject> tabs = new List<GameObject>();
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
