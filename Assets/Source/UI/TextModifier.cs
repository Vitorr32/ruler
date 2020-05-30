using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextModifier : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Text textComponent;

    public Color standardColor;
    public Color hightlightColor;

    // Start is called before the first frame update
    void Start() {
        textComponent = GetComponent<Text>();
        textComponent.color = standardColor;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        textComponent.color = hightlightColor;
    }

    public void OnPointerExit(PointerEventData eventData) {
        textComponent.color = standardColor;
    }
}
