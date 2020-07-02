using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TraitWrapper : MonoBehaviour, IPointerClickHandler
{
    public delegate void OnTraitWrapperClick(Trait trait);
    public static event OnTraitWrapperClick OnTraitWrapperClicked;

    public Image iconComponent;
    public Text textElement;

    private Trait associatedTrait;

    public void StartUpTraitWrapper(Trait trait) {
        iconComponent.sprite = trait.sprite;
        textElement.text = trait.name;

        associatedTrait = trait;
    }

    public void OnPointerClick(PointerEventData eventData) {
        OnTraitWrapperClicked?.Invoke(associatedTrait);
    }
}
