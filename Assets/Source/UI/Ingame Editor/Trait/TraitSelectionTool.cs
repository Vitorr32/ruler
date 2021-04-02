using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TraitSelectionTool : MonoBehaviour
{
    public List<SelectableTraitOption> selectableTraitOptions;
    public List<Trait> queriedTraits;

    public InputField inputField;
    public Trait selectedTrait;

    private int currentPage;
    public int pageSize = 10;
    public Button nextPage;
    public Button previousPage;

    // Start is called before the first frame update
    void Start() {
        SelectableTraitOption.OnTraitClicked += OnTraitSelected;
    }

    private void OnDestroy() {
        SelectableTraitOption.OnTraitClicked -= OnTraitSelected;
    }

    // Update is called once per frame
    void Update() {

    }

    public void OnSearchInputChanged() {
        string query = this.inputField.text;

        List<Trait> foundTraits = StoreController.instance.traits.FindAll((Trait trait) => {
            if (trait.name.Contains(query) || trait.id.ToString().Contains(query)) {
                return true;
            }

            return false;
        });

        this.queriedTraits = foundTraits;

        this.RenderSelectionableOptions();
    }

    private void OnTraitSelected(Trait trait) {
        this.selectedTrait = trait;
    }

    public void OnPagination(bool forward) {

    }

    private void RenderSelectionableOptions() {
        List<Trait> toShowTraits = this.queriedTraits.Skip(this.pageSize * this.currentPage).Take(this.pageSize).ToList();
        int indexOfSelected = toShowTraits.FindIndex((Trait trait) => trait.id == this.selectedTrait.id);

        for (int i = 0; i < pageSize; i++) {
            selectableTraitOptions[i].InitiateSelectableOption(toShowTraits.Count < i ? toShowTraits[i] : null, i == indexOfSelected);
        }
    }
}
