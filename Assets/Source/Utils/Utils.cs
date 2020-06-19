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

    public static List<T> GetEnumValues<T>() {
        T[] array = (T[])Enum.GetValues(typeof(T));

        //The first and last member of the Enum should always be UNDEFINED for the first, MAX_NUMBER for the former
        List<T> list = array.ToList();

        list.RemoveAt(0);
        list.RemoveAt(list.Count - 1);

        return list;
    }
}
