using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFB;
using UnityEditor;
using System.Linq;
using System.IO;

public enum GameFileImportDestination
{
    TRAIT,
    OFFICER
}
public static class GameFileImport
{
    private const string editorTraitPath = "/Resources/Images/Trait/";
    private const string editorOfficerPath = "/Resources/Images/Officer/";

    private const string deployTraitPath = "/UserContent/Trait/";
    private const string deployOfficerPath = "/UserContent/Officer/";

    public static Sprite ImportImageFromHost(GameFileImportDestination destination) {
        ExtensionFilter[] extensions = new[] { new ExtensionFilter("Image Files", "png", "jpg", "jpeg") };
        List<string> path = StandaloneFileBrowser.OpenFilePanel("Choose Image", "", extensions, false).ToList();

        if (path.Count == 0) {
            Debug.LogError("Error on importing the path");
            return null;
        }

        string fileName = path[0].Split('\\').Last();
        string pathToCopy;

        switch (destination) {
            case GameFileImportDestination.OFFICER:
                pathToCopy = (Application.isEditor ? Application.dataPath + editorOfficerPath : Application.persistentDataPath + deployOfficerPath) + fileName;
                break;
            case GameFileImportDestination.TRAIT:
                pathToCopy = (Application.isEditor ? Application.dataPath + editorTraitPath : Application.persistentDataPath + deployTraitPath) + fileName;
                break;
            default:
                Debug.LogError("Destination of import not set!");
                return null;
        }

        FileUtil.ReplaceFile(path[0], pathToCopy);

        return ConvertFileToSprite(pathToCopy, fileName.Remove(fileName.LastIndexOf('.')));
    }

    private static Sprite ConvertFileToSprite(string filePath, string name, int pixelsPerUnity = 100) {
        if (File.Exists(filePath)) {
            Texture2D texture2D = LoadTexture(filePath);
            Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0, 0), pixelsPerUnity);
            sprite.name = name;
            return sprite;
        }
        else {
            return null;
        }
    }

    private static Texture2D LoadTexture(string FilePath) {
        // Load a PNG or JPG file from disk to a Texture2D
        // Returns null if load fails

        Texture2D Tex2D;
        byte[] FileData;

        if (File.Exists(FilePath)) {
            FileData = File.ReadAllBytes(FilePath);
            Tex2D = new Texture2D(2, 2);           // Create new "empty" texture
            if (Tex2D.LoadImage(FileData))           // Load the imagedata into the texture (size is set automatically)
                return Tex2D;                 // If data = readable -> return texture
        }
        return null;                     // Return null if load failed
    }

}
