using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectableCharacterOption : SelectableOption<CharacterController>
{
    public Text charName;
    public Text age;
    public Text birthday;
    public Text id;
    public Text gender;
    public Text race;
    public Image charIcon;

    public override void InitiateSelectableOption(CharacterController charController, bool isSelected) {
        this.background = this.gameObject.GetComponent<Image>();

        if (charController == null) {
            this.associatedSelection = null;

            this.charName.text = "";
            this.age.text = "";
            this.birthday.text = "";
            this.id.text = "";
            this.gender.text = "";
            this.race.text = "";

            this.charIcon.sprite = null;
            this.background.color = new Color(0, 0, 0);
            this.gameObject.SetActive(false);
        }
        else {
            this.associatedSelection = charController;

            this.charName.text = charController.baseCharacter.name + " " + charController.baseCharacter.surname;
            this.age.text = charController.baseCharacter.age.ToString();
            this.birthday.text = charController.baseCharacter.birthday.ToString();
            this.id.text = charController.baseCharacter.id.ToString();
            this.gender.text = charController.baseCharacter.gender.ToString();
            this.race.text = charController.baseCharacter.race.ToString();

            this.charIcon.sprite = charController.charSprites[0].sprite;
            this.background.color = isSelected ? new Color(0, 255, 0) : new Color(255, 255, 255);
            this.gameObject.SetActive(true);
        }
    }
}
