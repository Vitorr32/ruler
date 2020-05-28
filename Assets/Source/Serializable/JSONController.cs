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
        public List<T> values = new List<T>();
    }

    public IEnumerator ParseFileListIntoType(List<TextAsset> textAssets) {
        List<List<T>> objectList = new List<List<T>>();

        if (textAssets.Count == 0) {
            Debug.Log("No text asset hsa been set for type " + typeof(T).FullName + "!");
        }

        foreach (TextAsset textAsset in textAssets) {
            JSONUtilityWrapper jsonWrapper = JsonUtility.FromJson<JSONUtilityWrapper>("{\"values\":" + textAsset.text + "}");

            objectList.Add(jsonWrapper.values);
        }

        resultList = objectList.SelectMany(d => d).ToList();

        yield return resultList;
    }
}
