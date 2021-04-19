using System.Collections.Generic;

public class CharacterSelectionTool : ListSelectionTool<CharacterStateController>
{
    protected override void SetInitiateToolValues() {
        this.currentPage = 0;
        this.maxPages = 0;
        this.pageSize = 20;
    }

    protected override List<CharacterStateController> GetAllSelectableMembers() {
        return StoreController.instance.characterControllers;
    }

    protected override List<CharacterStateController> FilterSelectableMembersByQuery(string query) {
        return StoreController.instance.characterControllers.FindAll((CharacterStateController character) => {
            if (
                (character.baseCharacter.name + character.baseCharacter.surname).Contains(query) ||
                character.baseCharacter.id.ToString().Contains(query)) {
                return true;
            }

            return false;
        });
    }

    protected override int GetIndexOfSelectionInList(CharacterStateController selected) {
        return this.queriedSelection.FindIndex((CharacterStateController character) => character.baseCharacter.id == selected.baseCharacter.id);
    }
}
