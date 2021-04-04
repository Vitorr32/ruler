using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectableTraitOption : SelectableOption<Trait>
{
    public Text description;
    public Text title;
    public Text type;
    public Text id;
    public Image icon;

    public override void InitiateSelectableOption(Trait trait, bool isSelected) {
        this.background = this.gameObject.GetComponent<Image>();

        if (trait == null) {
            this.associatedSelection = null;

            this.description.text = "";
            this.title.text = "";
            this.type.text = "";
            this.id.text = "";

            this.icon.sprite = null;
            this.background.color = new Color(0, 0, 0);
            this.gameObject.SetActive(false);
        }
        else {
            this.associatedSelection = trait;

            this.description.text = trait.description;
            this.title.text = trait.name;
            this.type.text = trait.type.ToString();
            this.id.text = trait.id.ToString();

            this.icon.sprite = trait.sprite;
            this.background.color = isSelected ? new Color(0, 255, 0) : new Color(255, 255, 255);
            this.gameObject.SetActive(true);
        }
    }

}
