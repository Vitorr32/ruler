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

    //Whatever this sprite template is a fixed member of the UI(such as the sprite selection on the editor) or not
    private bool isFixedUI = false;

    public void StartUpSpriteTemplate(OfficerSprite officerSpriteToSet) {
        UIText.text = officerSpriteToSet.filename;
        image.sprite = officerSpriteToSet.sprite;

        officerSprite = officerSpriteToSet;
    }

    public void OnPointerClick(PointerEventData eventData) {
        onSpriteTemplateClicked?.Invoke(officerSprite);
    }
}
