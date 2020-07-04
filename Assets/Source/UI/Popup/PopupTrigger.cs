using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PopupTrigger : MonoBehaviour
{
    //This script is a component of a gameobject that contains the content of the popup
    public PopupWrapper popupWrapper;

    public void OnTriggered() {
        popupWrapper.gameObject.SetActive(true);
    }
}
