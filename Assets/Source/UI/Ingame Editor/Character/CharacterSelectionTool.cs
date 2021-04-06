using System.Collections.Generic;

public class CharacterSelectionTool : ListSelectionTool<OfficerController>
{
    protected override void SetInitiateToolValues() {
        this.currentPage = 0;
        this.maxPages = 0;
        this.pageSize = 20;
    }

    protected override List<OfficerController> GetAllSelectableMembers() {
        return StoreController.instance.officers;
    }

    protected override List<OfficerController> FilterSelectableMembersByQuery(string query) {
        return StoreController.instance.officers.FindAll((OfficerController character) => {
            if (character.baseOfficer.firstName.Contains(query) || character.baseOfficer.id.ToString().Contains(query)) {
                return true;
            }

            return false;
        });
    }

    protected override int GetIndexOfSelectionInList(OfficerController selected) {
        return this.queriedSelection.FindIndex((OfficerController character) => character.baseOfficer.id == selected.baseOfficer.id);
    }
}
