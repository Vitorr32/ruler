using System.Collections.Generic;

public class CharacterSelectionTool : ListSelectionTool<CharacterController>
{
    protected override void SetInitiateToolValues() {
        this.currentPage = 0;
        this.maxPages = 0;
        this.pageSize = 20;
    }

    protected override List<CharacterController> GetAllSelectableMembers() {
        return StoreController.instance.characterControllers;
    }

    protected override List<CharacterController> FilterSelectableMembersByQuery(string query) {
        return StoreController.instance.characterControllers.FindAll((CharacterController character) => {
            if (
                (character.baseCharacter.name + character.baseCharacter.surname).Contains(query) ||
                character.baseCharacter.id.ToString().Contains(query)) {
                return true;
            }

            return false;
        });
    }

    protected override int GetIndexOfSelectionInList(CharacterController selected) {
        return this.queriedSelection.FindIndex((CharacterController character) => character.baseCharacter.id == selected.baseCharacter.id);
    }
}
