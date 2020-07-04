using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupWrapper : MonoBehaviour
{
    private GameObject backdrop;

    void Awake() {
        InstantiateBackdrop();
    }

    void InstantiateBackdrop() {
        backdrop = new GameObject("Backdrop");

        Backdrop tempInvisibleBGCtrl = backdrop.AddComponent<Backdrop>();
        tempInvisibleBGCtrl.SetParent(this);

        Image tempImage = backdrop.AddComponent<Image>();
        tempImage.color = new Color(1f, 1f, 1f, 0f);

        RectTransform tempTransform = backdrop.GetComponent<RectTransform>();
        tempTransform.anchorMin = new Vector2(0f, 0f);
        tempTransform.anchorMax = new Vector2(1f, 1f);
        tempTransform.offsetMin = new Vector2(0f, 0f);
        tempTransform.offsetMax = new Vector2(0f, 0f);
        tempTransform.SetParent(GetComponentsInParent<Transform>()[1], false);
        tempTransform.SetSiblingIndex(transform.GetSiblingIndex());
    }

    private void OnEnable() {
        backdrop.SetActive(true);
    }

    public void HidePopup() {
        gameObject.SetActive(false);
        backdrop.SetActive(false);
    }
}
