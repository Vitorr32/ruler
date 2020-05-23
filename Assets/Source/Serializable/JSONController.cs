using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class JSONController<T>
{
    public float proguess = 0;
    public List<T> resultList;

    private class JSONUtilityWrapper
    {
        public List<T> values;
    }

    public IEnumerator ParseFileListIntoType(List<TextAsset> textAssets) {
        List<List<T>> objectList = new List<List<T>>();

        foreach (TextAsset textAsset in textAssets) {
            Debug.Log(textAsset.text);
            JSONUtilityWrapper jsonWrapper = JsonUtility.FromJson<JSONUtilityWrapper>("{\"values\":" + textAsset.text + "}");

            objectList.Add(jsonWrapper.values);
        }

        resultList = objectList.SelectMany(d => d).ToList();

        yield return resultList;
    }
}
