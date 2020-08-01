using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionableSelector : MonoBehaviour
{
    public delegate void OnDelete(string context, int identifier);
    public static event OnDelete onSelectorDeleteTrigger;

    public delegate void OnEdit(string contenxt, int identifier);
    public static event OnDelete onSelectorEditTrigger;

    //Actionable selector Configuration
    private dynamic content;
    private int identifier;
    private string context;
    private bool editable;
    private bool deletable;

    //Actionable selector visual content
    private string title;
    private string description;

    //Actionable selector visual components
    public Text titleComponent;
    public Text descriptionComponent;
    public Button editButtonComponent;
    public Button deleteButtonComponent;

    public void OnSelectorStartup(dynamic content, int identifier, string context, string title, string description = null, bool editable = true, bool deletable = true) {
        this.content = content;
        this.identifier = identifier;
        this.context = context;
        this.editable = editable;
        this.deletable = deletable;

        UpdateComponentVisual();
    }

    private void UpdateComponentVisual() {
        this.editButtonComponent.gameObject.SetActive(this.editable);
        this.deleteButtonComponent.gameObject.SetActive(this.deletable);
        this.descriptionComponent.gameObject.SetActive(this.description != null);

        this.titleComponent.text = this.title;
        this.descriptionComponent.text = this.description != null ? this.description : "";
    }

    public dynamic GetSelectorContent() {
        return this.content;
    }

    public void OnSelectorDelete() {
        if (!this.deletable) { return; }

        onSelectorDeleteTrigger?.Invoke(this.context, this.identifier);
    }

    public void OnSelectorEdit() {
        if (!this.editable) { return; }
    }
}
