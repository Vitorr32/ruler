using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteList : MonoBehaviour
{
    public enum SpriteFilter
    {
        TEEN,
        YOUNG,
        MIDDLE_AGE,
        MALE,
        FEMALE
    }

    List<Sprite> sprites;
    public Transform spriteListAchor;
    public GameObject spriteTemplatePrefab;

    private void Awake() {
        SpriteTemplate.onSpriteTemplateClicked += SpriteTemplateSelected;
        ShowAllSprites();
    }

    private void ShowAllSprites() {
        StoreController.instance.sprites.ForEach(sprite => {
            GameObject spriteTemplate = Instantiate(spriteTemplatePrefab, spriteListAchor);

            spriteTemplate.GetComponent<SpriteTemplate>().StartUpSpriteTemplate(sprite, sprite.name);
        });
    }

    private void SpriteTemplateSelected(Sprite sprite, string filename) {

    }

}
