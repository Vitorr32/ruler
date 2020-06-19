using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    public Toggle toggle;
    public Image image;

    public void OnTooglePressed() {
        if (toggle.isOn) {
            image.color = Color.white;
        }
        else {
            image.color = Color.gray;
        }
    }

    public bool IsOn() {
        return toggle.isOn;
    }

    public void SetToggleStatus(bool isOn) {
        toggle.SetIsOnWithoutNotify(isOn);
        OnTooglePressed();
    }
}
