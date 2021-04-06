using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectableCharacterOption : SelectableOption<OfficerController>
{
    public Text description;
    public Text title;
    public Text type;
    public Text id;
    public Image icon;

    public override void InitiateSelectableOption(OfficerController officer, bool isSelected) {
        this.background = this.gameObject.GetComponent<Image>();

        if (officer == null) {
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
            this.associatedSelection = officer;

            //this.description.text = officer.ba.description;
            //this.title.text = officer.name;
            //this.type.text = officer.type.ToString();
            //this.id.text = officer.id.ToString();

            //this.icon.sprite = officer.sprite;
            this.background.color = isSelected ? new Color(0, 255, 0) : new Color(255, 255, 255);
            this.gameObject.SetActive(true);
        }
    }
}
