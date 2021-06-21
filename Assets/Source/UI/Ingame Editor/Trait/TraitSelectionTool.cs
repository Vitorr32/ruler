using System.Collections.Generic;
using System.Linq;

public class TraitSelectionTool : ListSelectionTool<Trait>
{
    protected override void SetInitiateToolValues() {
        this.currentPage = 0;
        this.maxPages = 0;
        this.pageSize = 20;
    }

    protected override List<Trait> GetAllSelectableMembers() {
        return StoreController.instance.traits;
    }

    protected override List<Trait> FilterSelectableMembersByQuery(string query) {
        return StoreController.instance.traits.FindAll((Trait trait) => {
            if (trait.name.Contains(query) || trait.id.ToString().Contains(query)) {
                return true;
            }

            return false;
        });
    }

    protected override List<int> GetIndexesOfSelectionInList(List<Trait> selectedTraits) {
        return selectedTraits.Select(selected => this.queriedSelection.FindIndex(
            (Trait trait) => trait.id == selected.id)
        ).ToList();
    }

    protected override int GetIndexOfElementOnArray(Trait element, List<Trait> array) {
        return array.FindIndex(trait => trait.id == element.id);
    }

}
