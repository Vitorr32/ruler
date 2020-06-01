using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class DialoguePooler
{
    public static string GetDialogueFromPool(DialogueType type, OfficerController speaker) {
        switch (type) {
            case DialogueType.INTRODUCTION:
                return "";
            default:
                Debug.LogError("Unknown type" + type);
                return "No Dialogue Found";
        }
    }
}
