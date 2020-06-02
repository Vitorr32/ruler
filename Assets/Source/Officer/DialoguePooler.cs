using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public static class DialoguePooler
{
    public static Dialogue ObtainDialogueTypeFromSpeaker(DialogueType type, OfficerController speaker) {
        switch (type) {
            case DialogueType.INTRODUCTION:
                return GetDialogueFromPool(GetPollOfDialogues(type), speaker);
            default:
                Debug.LogError("Unknown type" + type);
                return null;
        }
    }

    private static List<Dialogue> GetPollOfDialogues(DialogueType type) {        
        return StoreController.instance.dialogues.Where(dialogue => dialogue.type == type).ToList();
    }

    private static Dialogue GetDialogueFromPool(List<Dialogue> dialogues, OfficerController speaker) {
        float highestWeigth = 0;
        int indexOfHighestWeigth = 0;

        dialogues.ForEach(dialogue => {
            CalculateWeigthOfDialogueForSpeaker(dialogue, speaker);
        });

        return dialogues[indexOfHighestWeigth];
    }

    private static float CalculateWeigthOfDialogueForSpeaker(Dialogue dialogue, OfficerController speaker) {
        if (!IsDialogueRestrictedForSpeaker(dialogue.restrictions, speaker)) { return -999; }

        float weigth = 1;

        if (dialogue.moodModifier != 0.0f) {
            weigth += dialogue.moodModifier * speaker.baseOfficer.mood;
        }

        if (dialogue.traitPushes != null && dialogue.traitPushes.Count != 0) {
            dialogue.traitPushes.ForEach(traitPush => {
                if (speaker.serializedTraits.Find(trait => trait.id == traitPush.traitID) != null) {
                    weigth += traitPush.weight;
                }
            });
        }

        return weigth;
    }

    private static bool IsDialogueRestrictedForSpeaker(List<Restriction> restrictions, OfficerController speaker) {
        foreach (Restriction restriction in restrictions) {
            switch (restriction.type) {
                case Restriction.Type.HAS_TRAIT:
                    return true;
                default:
                    Debug.LogError("Unknown restriction type" + restriction.type);
                    return true;
            }
        }

        return false;
    }
}
