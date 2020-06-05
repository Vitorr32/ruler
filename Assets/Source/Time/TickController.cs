using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class TickController : MonoBehaviour
{
    public static TickController instance;
    //Nowhere else should be allowed to change the date value besides this class
    public static DateTime date { get; private set; }
    public bool gameIsPaused { get; set; }
    public bool onProgrammedTick { get; set; }

    public enum Speed
    {
        PAUSED,
        HALF,
        NORMAL,
        DOUBLE,
        REAL_TIME,

        MAX_SPEEDS
    }
    public enum Flag
    {
        NO_FLAG,
        FIFTEEN_MIN_PASSED,
        HALF_HOUR_PASSED,
        HOUR_PASSED,
        DAY_PASSED,
        MONTH_PASSED,
        YEAR_PASSED,

        MAX_FLAGS
    }
    public Speed currentSpeed { get; private set; } = Speed.DOUBLE;

    //How much time a second in real life is to minutes in the game clock
    [SerializeField] private float secondToMinuteBase = 1f;
    private float sinceLastTick = 0f;

    public delegate void OnTime(DateTime date, Speed speed);
    public static event OnTime onTimeAdvance;

    public delegate void OnTick(Flag flag);
    public static event OnTick onFlagTriggered;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        }
        else {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start() {
        date = new DateTime(1000, 1, 1);

        sinceLastTick = getTickSpeed(currentSpeed);
    }

    // Update is called once per frame
    void Update() {
        if (gameIsPaused) { return; }

        if (currentSpeed != Speed.PAUSED) {
            DateTime previousTime = date;
            updateDate(currentSpeed);

            //When on realtime, there's no need to check if the time since the last tick has passed, just send the events
            if (currentSpeed == Speed.REAL_TIME) {
                onFlagTriggered?.Invoke(getFlag(previousTime, date));
                onTimeAdvance?.Invoke(date, currentSpeed);
                return;
            }

            sinceLastTick -= Time.deltaTime * getTickSpeed(currentSpeed);

            if (sinceLastTick <= 0) {
                sinceLastTick += getTickSpeed(currentSpeed);

                onFlagTriggered?.Invoke(getFlag(previousTime, date));
            }

            onTimeAdvance?.Invoke(date, currentSpeed);
        }
    }

    public void MoveTickXTimes(int ticks) {
        if (onProgrammedTick) {
            Debug.LogError("Programatically tick called before previous one finished");
            return;
        }

        onProgrammedTick = true;

        StartCoroutine(TickXTimes(ticks));
    }

    private IEnumerator TickXTimes(int ticks) {
        int counter = 0;

        while (ticks > counter) {
            DateTime previousTime = date;
            updateDate(1);
            onFlagTriggered?.Invoke(getFlag(previousTime, date));
            onTimeAdvance?.Invoke(date, currentSpeed);

            yield return new WaitForSeconds(0.2f);
        }

        onProgrammedTick = false;

        yield return null;
    }

    private void updateDate(Speed speed = Speed.REAL_TIME) {
        if (speed == Speed.REAL_TIME) {
            //Time.deltaTime is seconds since last frame, so we multiply it by 60 to get how many minutes in-game passed since last frame
            date = date.AddMinutes(Time.deltaTime * secondToMinuteBase * 60);
        }
        else {
            //Otherwise we use the regular speeds multipliers
            date = date.AddMinutes(Time.deltaTime * secondToMinuteBase * getTickSpeed(currentSpeed));
        }
    }

    private void updateDate(int minutes) {
        date = date.AddMinutes(minutes);
    }

    private Flag getFlag(DateTime previousTick, DateTime currentDate) {
        if (currentDate.Year != previousTick.Year) {
            return Flag.YEAR_PASSED;
        }

        if (currentDate.Month != previousTick.Month) {
            return Flag.MONTH_PASSED;
        }

        if (currentDate.Day != previousTick.Day) {
            return Flag.DAY_PASSED;
        }

        if (currentDate.Hour != previousTick.Hour) {
            return Flag.HOUR_PASSED;
        }

        if (currentDate.Minute >= 30 && previousTick.Minute < 30) {
            return Flag.HALF_HOUR_PASSED;
        }

        //Check if the current minute value will trigger a ten minutes passed flag
        if ((currentDate.Minute >= 15 && previousTick.Minute < 15) || (currentDate.Minute >= 45 && previousTick.Minute < 45)) {
            return Flag.FIFTEEN_MIN_PASSED;
        }

        return Flag.NO_FLAG;
    }

    public void changeGameSpeed(Speed newSpeed) {
        currentSpeed = newSpeed;
    }

    public float getTickSpeed(Speed speed) {
        switch (speed) {
            case Speed.PAUSED:
                return 0;
            case Speed.HALF:
                return 1f;
            case Speed.NORMAL:
                return 2.5f;
            case Speed.DOUBLE:
                return 5.0f;
            case Speed.REAL_TIME:
                return 60f;
            default:
                throw new Exception("Unknown speed " + speed);
        }
    }
}
