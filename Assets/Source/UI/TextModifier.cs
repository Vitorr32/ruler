using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextModifier : MonoBehaviour
{
    private Text textComponent;
    // Start is called before the first frame update
    void Start() {
        textComponent = GetComponent<Text>();
    }

    public void ChangeColorOfText(string newColorString) {
        string[] rgba = newColorString.Split(',');

    }
}
