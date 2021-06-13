using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectableAttributeOption : SelectableOption<Attribute>
{
    public Text description;
    public Text title;
    public Text category;
    public Text growth;
    public Text id;

    public override void InitiateSelectableOption(Attribute attr, bool isSelected) {
        this.background = this.gameObject.GetComponent<Image>();

        if (attr == null) {
            this.associatedSelection = null;

            this.description.text = "";
            this.title.text = "";
            this.category.text = "";
            this.id.text = "";

            this.background.color = new Color(0, 0, 0);
            this.gameObject.SetActive(false);
        }
        else {
            this.associatedSelection = attr;

            this.description.text = attr.description;
            this.title.text = attr.name;
            this.category.text = attr.category.ToString();
            this.id.text = attr.id.ToString();

            this.background.color = isSelected ? new Color(0, 255, 0) : new Color(255, 255, 255);
            this.gameObject.SetActive(true);
        }
    }

}
