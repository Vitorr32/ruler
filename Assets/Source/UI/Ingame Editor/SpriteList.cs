using System.Collections;
using System.Collections.Generic;
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

    private void OnSpriteSelectionRequest(OfficerSprite.Age age, OfficerSprite.Gender gender) {
        gameObject.SetActive(true);


        genderTagsToFilter =
            gender == OfficerSprite.Gender.UNDEFINED
                ? new List<OfficerSprite.Gender>()
                : new List<OfficerSprite.Gender>() { gender };

        ageTagsToFilter =
            age == OfficerSprite.Age.UNDEFINED
                ? new List<OfficerSprite.Age>()
                : new List<OfficerSprite.Age>() { age };

        OnSetInitialToggleState(age, gender);

        FilterSpriteListByTag(ageTagsToFilter, genderTagsToFilter);
    }

    private void OnSetInitialToggleState(OfficerSprite.Age age, OfficerSprite.Gender gender) {
        male.SetToggleStatus(gender == OfficerSprite.Gender.UNDEFINED || gender == OfficerSprite.Gender.MALE);
        female.SetToggleStatus(gender == OfficerSprite.Gender.UNDEFINED || gender == OfficerSprite.Gender.FEMALE);

        teen.SetToggleStatus(age == OfficerSprite.Age.UNDEFINED || age == OfficerSprite.Age.TEEN);
        young.SetToggleStatus(age == OfficerSprite.Age.UNDEFINED || age == OfficerSprite.Age.YOUNG);
        middleAge.SetToggleStatus(age == OfficerSprite.Age.UNDEFINED || age == OfficerSprite.Age.MIDDLE_AGE);
    }

    public void FilterSpriteListByTag(List<OfficerSprite.Age> ageTags, List<OfficerSprite.Gender> genderTags) {

        Debug.Log(genderTags.Count);
        Debug.Log(genderTags[0]);

        spriteTemplates.ForEach(spriteTemplate => {
            OfficerSprite officerSprite = spriteTemplate.officerSprite;

            bool shouldShowByAge =
                spriteTemplate.officerSprite.age == OfficerSprite.Age.UNDEFINED ||
                ageTags.Count == 0 ||
                ageTags.Contains(officerSprite.age);


            bool shouldShowByGender =
                spriteTemplate.officerSprite.gender == OfficerSprite.Gender.UNDEFINED ||
                genderTags.Count == 0 ||
                genderTags.Contains(officerSprite.gender);

            Debug.Log(officerSprite.age);
            Debug.Log(shouldShowByAge);
            Debug.Log(shouldShowByGender);
            Debug.Log(shouldShowByAge && shouldShowByGender);

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

            spriteTemplate.StartUpSpriteTemplate(sprite, true);
            spriteTemplates.Add(spriteTemplate);
        });
    }


}
