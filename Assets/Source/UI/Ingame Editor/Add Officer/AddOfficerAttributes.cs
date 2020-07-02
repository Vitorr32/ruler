using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddOfficerAttributes : MonoBehaviour
{
    public Text remaningPointsText;
    public Dropdown tierDropdown;
    public List<NumericInputCarousel> attributeCarrousels;

    private int maxPointsValue;

    // Start is called before the first frame update
    void Start() {
        tierDropdown.onValueChanged.AddListener(delegate {
            OnTierBeenChoosen(tierDropdown);
        });

        NumericInputCarousel.onCarouselWasInteracted += AttributeWasChanged;
    }

    private void OnDestroy() {
        NumericInputCarousel.onCarouselWasInteracted -= AttributeWasChanged;
    }
    public void OnTierBeenChoosen(Dropdown tierDropdown) {
        Officer.Tier tier = (Officer.Tier)Enum.Parse(typeof(Officer.Tier), tierDropdown.options[tierDropdown.value].text);

        maxPointsValue = (int)tier;

        for (int i = 0; i < (int)Officer.Attribute.MAX_ATTRIBUTES; i++) {
            attributeCarrousels[i].SetUpAttributeCarousel(i);
        }

        UpdateRemainingPointsLeft();
    }

    private void AttributeWasChanged(int currentValue, int changedValue, int carouselIdentifier, bool directInput) {
        int currentValuesInCarousel = ReduceCurrentCarouselValues();

        if (directInput) {
            //If the changed value will be superior to the maximum point when added to the current total
            if ((currentValuesInCarousel - currentValue) + changedValue >= maxPointsValue) {
                attributeCarrousels[carouselIdentifier].OnSetCarouselValue(maxPointsValue - (currentValuesInCarousel - currentValue));
            }
            else if (changedValue < 0) {
                attributeCarrousels[carouselIdentifier].OnSetCarouselValue(0);
            }
            else {
                attributeCarrousels[carouselIdentifier].OnSetCarouselValue(changedValue);
            }
        }
        else {
            //If the offset will make the value go into the negatives, just pick 0
            if (currentValue + changedValue < 0) {
                attributeCarrousels[carouselIdentifier].OnSetCarouselValue(0);
            }
            //If the addition of the offset will make the total surpass the maximum.
            //Just use the offset between the maximum and the current instead
            else if (currentValuesInCarousel + changedValue > maxPointsValue) {
                attributeCarrousels[carouselIdentifier].OnSetCarouselValue(currentValue + (maxPointsValue - currentValuesInCarousel));
            }
            //If the value does not surpass the maximum or goes bellow the negative, just calculate normally
            else {
                attributeCarrousels[carouselIdentifier].OnSetCarouselValue(attributeCarrousels[carouselIdentifier].value + changedValue);
            }
        }
        UpdateRemainingPointsLeft();
    }

    private void UpdateRemainingPointsLeft() {
        remaningPointsText.text = (maxPointsValue - ReduceCurrentCarouselValues()).ToString();
    }

    private int ReduceCurrentCarouselValues() {
        int reducedValue = 0;

        foreach (NumericInputCarousel attributeCarousel in attributeCarrousels) {
            reducedValue += attributeCarousel.value;
        }

        return reducedValue;
    }


}
