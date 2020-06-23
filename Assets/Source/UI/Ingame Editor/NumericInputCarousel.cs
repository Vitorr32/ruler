using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NumericInputCarousel : MonoBehaviour
{
    public delegate void OnCarouselInteraction(int currentValue, int valueChange, int carouselIdentifier, bool directInput = false);
    public static event OnCarouselInteraction onCarouselWasInteracted;

    public int value;
    private int identifier;
    public InputField input;

    private void Start() {
    }

    public void SetUpAttributeCarousel(int toSetIdentifier, int startValue = 0) {
        identifier = toSetIdentifier;
        OnSetCarouselValue(startValue);
    }

    public void OnSetCarouselValue(int newValue) {
        value = newValue;
        input.text = newValue.ToString();
    }

    public void OnInputValueChanged(string newValue) {
        try {
            onCarouselWasInteracted?.Invoke(value, Int16.Parse(newValue), identifier, true);
        }
        catch (FormatException) {
            input.text = value.ToString();
            Console.WriteLine("Unable to parse '{0}'.", newValue);
        }
    }

    public void OnCarouselClick(bool isIncrease) {
        int offset = isIncrease ? 1 : -1;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
            offset *= 10;
        }
        else if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) {
            offset *= 5;
        }

        onCarouselWasInteracted?.Invoke(value, offset, identifier);
    }
}
