using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectableTraitOption : MonoBehaviour
{

    public delegate void OnTraitClick(Trait trait);
    public static event OnTraitClick OnTraitClicked;

    public Text description;
    public Text title;
    public Text type;
    public Text id;
    public Image icon;

    private Trait associatedTrait;
    private Image background;

    // Start is called before the first frame update
    void Start() {
        this.background = this.gameObject.GetComponent<Image>();
    }

    public void InitiateSelectableOption(Trait trait, bool isSelected) {
        if (trait == null) {
            this.associatedTrait = null;

            this.description.text = "";
            this.title.text = "";
            this.type.text = "";
            this.id.text = "";

            this.icon.sprite = null;
            this.background.color = new Color(255, 0, 0);
        }
        else {
            this.associatedTrait = trait;

            this.description.text = trait.description;
            this.title.text = trait.name;
            this.type.text = trait.type.ToString();
            this.id.text = trait.id.ToString();

            this.icon.sprite = trait.sprite;
            this.background.color = new Color(255, 255, 255);
        }
    }

    public void HighlightSelectableOption(bool shouldHighlight) {

    }

    public void OnSelectableClick() {
        OnTraitClicked?.Invoke(this.associatedTrait);
    }

}
