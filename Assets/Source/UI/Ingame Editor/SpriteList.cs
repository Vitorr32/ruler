using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class SpriteList : MonoBehaviour
{
    private List<SpriteTemplate> spriteTemplates = new List<SpriteTemplate>();

    public ToggleButton male;
    public ToggleButton female;
    public ToggleButton teen;
    public ToggleButton young;
    public ToggleButton middleAge;

    public List<ToggleButton> ageToggles;
    public List<ToggleButton> genderToggles;

    private List<OfficerSprite.Age> ageTagsToFilter;
    private List<OfficerSprite.Gender> genderTagsToFilter;

    public Transform spriteListAchor;
    public GameObject spriteTemplatePrefab;

    private void Awake() {
        ShowAllSprites();
        BasicInfoController.onSpriteSelectionRequest += OnSpriteSelectionRequest;

        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        BasicInfoController.onSpriteSelectionRequest -= OnSpriteSelectionRequest;
    }

    private void OnSpriteSelectionRequest(OfficerSprite.Age age, OfficerSprite.Gender gender) {
        gameObject.SetActive(true);

        genderTagsToFilter =
            gender == OfficerSprite.Gender.UNDEFINED
                ? Utils.GetEnumValues<OfficerSprite.Gender>()
                : new List<OfficerSprite.Gender>() { gender };

        ageTagsToFilter =
            age == OfficerSprite.Age.UNDEFINED
                ? Utils.GetEnumValues<OfficerSprite.Age>()
                : new List<OfficerSprite.Age>() { age };

        OnSetInitialToggleState(ageTagsToFilter, genderTagsToFilter);
        FilterSpriteListByTag(ageTagsToFilter, genderTagsToFilter);
    }

    private void OnSetInitialToggleState(List<OfficerSprite.Age> ageTagsToFilter, List<OfficerSprite.Gender> genderTagsToFilter) {
        male.SetToggleStatus(genderTagsToFilter.Contains(OfficerSprite.Gender.MALE));
        female.SetToggleStatus(genderTagsToFilter.Contains(OfficerSprite.Gender.FEMALE));

        teen.SetToggleStatus(ageTagsToFilter.Contains(OfficerSprite.Age.TEEN));
        young.SetToggleStatus(ageTagsToFilter.Contains(OfficerSprite.Age.YOUNG));
        middleAge.SetToggleStatus(ageTagsToFilter.Contains(OfficerSprite.Age.MIDDLE_AGE));
    }

    public void FilterSpriteListByTag(List<OfficerSprite.Age> ageTags, List<OfficerSprite.Gender> genderTags) {
        spriteTemplates.ForEach(spriteTemplate => {
            OfficerSprite officerSprite = spriteTemplate.officerSprite;

            bool shouldShowByAge =
                spriteTemplate.officerSprite.age == OfficerSprite.Age.UNDEFINED ||
                ageTags.Contains(officerSprite.age);

            bool shouldShowByGender =
                spriteTemplate.officerSprite.gender == OfficerSprite.Gender.UNDEFINED ||
                genderTags.Contains(officerSprite.gender);

            spriteTemplate.gameObject.SetActive(shouldShowByAge && shouldShowByGender);
        });
    }

    public void ToogleFilterValueForAge(int toFilterAge) {
        OfficerSprite.Age age = (OfficerSprite.Age)toFilterAge;

        if (ageTagsToFilter.Contains(age)) {
            ageTagsToFilter.Remove(age);
        }
        else {
            ageTagsToFilter.Add(age);
        }

        FilterSpriteListByTag(ageTagsToFilter, genderTagsToFilter);
    }

    public void ToogleFilterValueForGender(int toFilterGender) {
        OfficerSprite.Gender gender = (OfficerSprite.Gender)toFilterGender;

        if (genderTagsToFilter.Contains(gender)) {
            genderTagsToFilter.Remove(gender);
        }
        else {
            genderTagsToFilter.Add(gender);
        }

        FilterSpriteListByTag(ageTagsToFilter, genderTagsToFilter);
    }

    private void ShowAllSprites() {
        StoreController.instance.sprites.ForEach(sprite => {
            SpriteTemplate spriteTemplate = Instantiate(spriteTemplatePrefab, spriteListAchor).GetComponent<SpriteTemplate>();

            spriteTemplate.StartUpSpriteTemplate(sprite);
            spriteTemplates.Add(spriteTemplate);
        });
    }
}
