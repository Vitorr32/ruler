using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Backdrop : MonoBehaviour, IPointerClickHandler
{
    PopupWrapper popupParent;

    public void SetParent(PopupWrapper popupWrapper) {
        popupParent = popupWrapper;
    }

    public void OnPointerClick(PointerEventData eventData) {
        popupParent.HidePopup();
    }
}
