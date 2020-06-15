using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpriteTemplate : MonoBehaviour, IPointerClickHandler
{
    public delegate void OnClick(Sprite sprite, string filename);
    public static event OnClick onSpriteTemplateClicked;

    public string m_filename;
    public Sprite m_sprite;

    public Text uiText;
    public Image image;

    public void StartUpSpriteTemplate(Sprite sprite, string filename) {
        m_filename = filename;
        m_sprite = sprite;

        uiText.text = filename;
        image.sprite = sprite;
    }

    public void OnPointerClick(PointerEventData eventData) {
        onSpriteTemplateClicked?.Invoke(m_sprite, m_filename);
    }
}
