
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System;

public static class Summarizer
{
    public static string SummarizeEffect(Effect effect) {
        string summary = "";

        summary += SummarizeTrigger(effect.trigger);
        summary += SummarizeTarget(effect.target);
        summary += SummarizePrimaryTargetSelectors(effect);

        return summary;
    }

    private static string SummarizeTrigger(Effect.Trigger trigger) {
        switch (trigger.type) {
            case ActionType.ALWAYS_ACTIVE:
                return "";
            case ActionType.ON_INTERACTION:
                return "When interacting  ";
        }
        return "";
    }
    private static string SummarizeTarget(Effect.Target target) {
        switch (target.type) {
            case Effect.Target.Type.TARGET_ATTRIBUTE:
                return "Modifies the attribute(s) ";
            case Effect.Target.Type.TARGET_CHARACTER_BY_AGE:
                return "When the character is of age " + "";
        }
        return "Swag";
    }

    private static string SummarizePrimaryTargetSelectors(Effect effect) {
        switch (effect.target.type) {
            case Effect.Target.Type.TARGET_ATTRIBUTE:
                List<string> list = effect.target.arguments.Select(i => Enum.GetName(Officer.Attribute, i) ).ToList();

                return list.Join(",");
        }
    }
}