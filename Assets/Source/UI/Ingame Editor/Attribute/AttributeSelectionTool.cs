using System.Collections.Generic;
using UnityEngine;

public class AttributeSelectionTool : ListSelectionTool<Attribute>
{
    protected override void SetInitiateToolValues() {
        this.currentPage = 0;
        this.maxPages = 0;
        this.pageSize = 20;
    }

    protected override List<Attribute> GetAllSelectableMembers() {
        //return StoreController.instance.traits;
        return new List<Attribute>();
    }

    protected override List<Attribute> FilterSelectableMembersByQuery(string query) {
        //return StoreController.instance.traits.FindAll((Trait trait) => {
        //    if (trait.name.Contains(query) || trait.id.ToString().Contains(query)) {
        //        return true;
        //    }

        //    return false;
        //});
        return new List<Attribute>();
    }

    protected override int GetIndexOfSelectionInList(Attribute selected) {
        return this.queriedSelection.FindIndex((Attribute attr) => attr.id == selected.id);
    }

}
