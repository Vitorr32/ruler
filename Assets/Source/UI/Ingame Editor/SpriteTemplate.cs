using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpriteTemplate : MonoBehaviour, IPointerClickHandler
{

    public delegate void OnClick(OfficerSprite officerSprite);
    public static event OnClick onSpriteTemplateClicked;

    public Text UIText;
    public Image image;
    public OfficerSprite officerSprite;

    private bool shouldEmitEvents = false;

    public void StartUpSpriteTemplate(OfficerSprite officerSprite, bool shouldEmitEvent = false) {
        UIText.text = officerSprite.filename;
        image.sprite = officerSprite.sprite;

        shouldEmitEvents = shouldEmitEvent;
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (!shouldEmitEvents) { return; }

        onSpriteTemplateClicked?.Invoke(officerSprite);
    }
}
