using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class ClockController : MonoBehaviour
{
    // Start is called before the first frame update
    private Text clockDate;
    void Start() {
        clockDate = GetComponent<Text>();
        clockDate.text = TickController.date.ToString(Utils.displayDateFormat, new CultureInfo("en-US"));

        TickController.onTimeAdvance += onDateChange;
    }
    private void OnDestroy() {
        TickController.onTimeAdvance -= onDateChange;
    }

    private void onDateChange(DateTime date, TickController.Speed speed) {
        clockDate.text = date.ToString("ddd, dd MMMM yyyy HH:mm", new CultureInfo("en-US"));
    }
}
