using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public const string displayDateFormat = "ddd, dd MMMM yyyy HH:mm";
    public const string fullDateFormat = "dd-MM-yyyy HH:mm:ss";

    public static DateTime ConvertStringToGameDate(string dateString) {
        try {
            return DateTime.ParseExact(dateString, fullDateFormat, null);
        }
        catch (FormatException e) {
            Debug.LogError("Error on parsing date string " + dateString);
            Debug.Log(e);

            return new DateTime();
        }
    }

    public static string ConvertGameDateToString(DateTime date) {
        return date.ToString(fullDateFormat);
    }
}
