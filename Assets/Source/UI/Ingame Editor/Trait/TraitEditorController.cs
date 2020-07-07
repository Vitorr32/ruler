using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TraitEditorController : MonoBehaviour
{
    public Image image;
    public InputField nameField;
    public Dropdown type;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void ImportImageFromHostMachine() {
        this.image.sprite = GameFileImport.ImportImageFromHost(GameFileImportDestination.TRAIT);
    }
}
