using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeControl : MonoBehaviour
{
    [SerializeField] public TickController.Speed buttonSpeed = TickController.Speed.PAUSED;

    public void onTimeControllerClicked() {
        TickController.instance.changeGameSpeed(buttonSpeed);
    }
}
