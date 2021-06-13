using System.Collections.Generic;

public class AttributeSelectionTool : ListSelectionTool<Attribute>
{
    protected override void SetInitiateToolValues() {
        this.currentPage = 0;
        this.maxPages = 0;
        this.pageSize = 20;
    }

    protected override List<Attribute> GetAllSelectableMembers() {
        return StoreController.instance.attributes;
    }

    protected override List<Attribute> FilterSelectableMembersByQuery(string query) {
        return StoreController.instance.attributes.FindAll((Attribute attribute) => {
            if (attribute.name.Contains(query) || attribute.id.ToString().Contains(query)) {
                return true;
            }

            return false;
        });
    }

    protected override int GetIndexOfSelectionInList(Attribute selected) {
        return this.queriedSelection.FindIndex((Attribute attr) => attr.id == selected.id);
    }

}
