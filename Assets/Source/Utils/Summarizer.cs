
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using System.Runtime.CompilerServices;

public static class Summarizer
{
    public static string SummarizeEffect(Effect effect, bool allowIncomplete = false) {
        string summary = "";

        summary += SummarizeTrigger(effect.trigger);
        summary += SummarizeModifierTargets(effect.modifier, allowIncomplete);

        return summary;
    }

    private static string SummarizeTrigger(Effect.Trigger trigger) {
        switch (trigger) {
            case Effect.Trigger.ALWAYS_ACTIVE:
                return "";
            case Effect.Trigger.ON_INTERACTION_START:
                return "When interaction starts:  ";
        }
        return "";
    }
    private static string SummarizeTarget(Modifier modifier) {
        switch (modifier.type) {
            case Modifier.Type.MODIFY_SKILL_POTENTIAL_VALUE:
                return "Modifies the attribute(s) ";
            case Modifier.Type.MODIFY_SKILL_VALUE:
                return "When the character is of age " + "";
        }
        return "Swag";
    }

    private static string SummarizeModifierTargets(Modifier modifier, bool allowIncomplete = false) {
        if (modifier.modifierTargets.Count == 0) {
            if (allowIncomplete) {
                return "";
            }
            else {
                throw new Exception("The modifier has no targets, please check the effect");
            }
        }

        switch (modifier.type) {
            case Modifier.Type.MODIFY_SKILL_VALUE:
                string initial = "Modify " + (modifier.modifierTargets.Count > 1 ? "attributes" : "attribute") + Environment.NewLine;
                string finalString = "";

                foreach (int modifierTarget in modifier.modifierTargets) {
                    Skill skill = StoreController.instance.skills.Find(skill => skill.id == modifierTarget);
                    finalString += skill.name + " by" + Environment.NewLine;
                }

                return finalString;
            default:
                return allowIncomplete ? "" : "TARGET TYPE " + modifier.type + " NOT FOUND IN ENUMERATOR";
        }
    }

    public struct TargetAttributeArguments
    {
        public string name;
        public int absoluteChange;
        public float percentageChange;
    }
}