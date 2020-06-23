using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public static List<T> GetEnumValues<T>(bool returnAll = false) {
        List<T> array = ((T[])Enum.GetValues(typeof(T))).ToList();

        if (returnAll) {
            return array;
        }

        //The first and last member of the Enum should always be UNDEFINED for the first, MAX_NUMBER for the former
        array.RemoveAt(array.FindIndex((T value) => (int)(object)value == array.Count - 1));
        array.RemoveAt(array.FindIndex((T value) => (int)(object)value == -1));

        return array;
    }
}
