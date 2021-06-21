using System.Collections.Generic;
using System.Linq;

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

    protected override List<int> GetIndexesOfSelectionInList(List<Attribute> selectedAttributes) {
        return selectedAttributes.Select(selected => this.queriedSelection.FindIndex(
            (Attribute attr) => attr.id == selected.id)
        ).ToList();
    }
    protected override int GetIndexOfElementOnArray(Attribute element, List<Attribute> array) {
        return array.FindIndex(attribute => attribute.id == element.id);
    }

}
