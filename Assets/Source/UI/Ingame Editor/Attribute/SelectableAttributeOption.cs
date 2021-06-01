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

            this.gameObject.SetActive(false);
        }
        else {
            this.associatedSelection = attr;

            this.description.text = attr.description;
            this.title.text = attr.name;
            this.category.text = attr.category.ToString();
            this.id.text = attr.id.ToString();

            this.gameObject.SetActive(true);
        }
    }

}
