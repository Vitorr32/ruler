using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class BasicInfoController : MonoBehaviour
{
    public delegate void OnSpriteSelection(OfficerSprite.Age age, OfficerSprite.Gender gender);
    public static event OnSpriteSelection onSpriteSelectionRequest;

    public enum SpriteSelector
    {
        TEEN_SPRITE_SELECTOR,
        YOUNG_SPRITE_SELECTOR,
        MIDDLE_AGE_SPRITE_SELECTOR
    }

    public SpriteList spriteList;

    public Dropdown gender;
    public Dropdown race;
    public Dropdown religion;
    public Dropdown status;

    public SpriteTemplate teen;
    public SpriteTemplate young;
    public SpriteTemplate middleAge;

    private SpriteSelector currentSpriteSelector;

    void Start() {
        SpriteTemplate.onSpriteTemplateClicked += SpriteTemplateSelected;
    }

    private void OnDestroy() {
        SpriteTemplate.onSpriteTemplateClicked -= SpriteTemplateSelected;
    }

    // Update is called once per frame
    void Update() {

    }

    private void SpriteTemplateSelected(OfficerSprite officerSprite) {
        spriteList.gameObject.SetActive(false);

        switch (currentSpriteSelector) {
            case SpriteSelector.TEEN_SPRITE_SELECTOR:
                teen.StartUpSpriteTemplate(officerSprite);
                break;
            case SpriteSelector.YOUNG_SPRITE_SELECTOR:
                young.StartUpSpriteTemplate(officerSprite);
                break;
            case SpriteSelector.MIDDLE_AGE_SPRITE_SELECTOR:
                middleAge.StartUpSpriteTemplate(officerSprite);
                break;
        }
    }

    public void OnSpriteSelectorClicked(int spriteSelectorInteger) {
        if (spriteList.gameObject.activeSelf && currentSpriteSelector == (SpriteSelector)spriteSelectorInteger) {
            spriteList.gameObject.SetActive(false);
            return;
        }

        currentSpriteSelector = (SpriteSelector)spriteSelectorInteger;

        switch (currentSpriteSelector) {
            case SpriteSelector.TEEN_SPRITE_SELECTOR:
                onSpriteSelectionRequest?.Invoke(OfficerSprite.Age.TEEN, (OfficerSprite.Gender)gender.value);
                break;
            case SpriteSelector.YOUNG_SPRITE_SELECTOR:
                onSpriteSelectionRequest?.Invoke(OfficerSprite.Age.YOUNG, (OfficerSprite.Gender)gender.value);
                break;
            case SpriteSelector.MIDDLE_AGE_SPRITE_SELECTOR:
                onSpriteSelectionRequest?.Invoke(OfficerSprite.Age.MIDDLE_AGE, (OfficerSprite.Gender)gender.value);
                break;
        }
    }
}
