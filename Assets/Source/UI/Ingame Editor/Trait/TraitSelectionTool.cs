using System.Collections.Generic;
using UnityEngine;

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

    protected override int GetIndexOfSelectionInList(Trait selected) {
        return this.queriedSelection.FindIndex((Trait trait) => trait.id == selected.id);
    }

}
