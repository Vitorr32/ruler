using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageModifier : MonoBehaviour
{
    private Image image;

    public void Start() {
        image = GetComponent<Image>();

        if (!image) {
            Debug.Log("Image modifer put in a gameobject wihtout image component!");           
        }
    }

    public void ChangeColor() {

    }
}
