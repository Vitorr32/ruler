using System;
using System.Collections.Generic;
using System.Linq;

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

    protected override List<int> GetIndexesOfSelectionInList(List<CharacterStateController> selectedCharacters) {
        return selectedCharacters.Select(selected => this.queriedSelection.FindIndex(
            (CharacterStateController character) => character.baseCharacter.id == selected.baseCharacter.id)
        ).ToList();
    }

    protected override int GetIndexOfElementOnArray(CharacterStateController element, List<CharacterStateController> array) {
        return array.FindIndex(character => character.baseCharacter.id == element.baseCharacter.id);
    }
}
