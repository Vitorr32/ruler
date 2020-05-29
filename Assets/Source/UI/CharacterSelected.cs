using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelected : MonoBehaviour
{
    OfficerController m_selectedOfficer;
    public Text m_name;
    public Text m_age;
    public Text m_traits;
    // Start is called before the first frame update
    void Start() {
        PieceController.onActorClicked += onCharacterSelection;
    }

    void OnDestroy() {
        PieceController.onActorClicked -= onCharacterSelection;
    }

    // Update is called once per frame
    void Update() {

    }

    void onCharacterSelection(OfficerController controller) {
        m_selectedOfficer = controller;
        populateDataFromOfficer(controller.baseOfficer);
    }

    void populateDataFromOfficer(Officer officer) {
        /*
        m_name.text = officer.firstName + ' ' + officer.familyName;
        m_age.text = (DateTime.Now.AddYears(18) - officer.birth).ToString();

        m_selectedOfficer.serializedTraits.ForEach(trait => m_traits.text.Concat(trait.id.ToString()));
        */
    }
}
