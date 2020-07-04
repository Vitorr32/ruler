using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFB;
using UnityEditor;
using System.Linq;

public class SpriteImportController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        var extensions = new[] { new ExtensionFilter("Image Files", "png", "jpg", "jpeg") };
        List<string> path = StandaloneFileBrowser.OpenFilePanel("Choose Image", "", extensions, false).ToList();

        if (path.Count == 0) {
            Debug.LogError("Error on importing the path");
            return;
        }

        FileUtil.CopyFileOrDirectory(path[0], Application.dataPath + "/Resources/Images/Trait/" + path[0].Split('\\').Last());
        Debug.Log(path[0]);
    }
}
