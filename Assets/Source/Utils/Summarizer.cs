
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public static class Summarizer
{
    public static string SummarizeEffect(Effect effect, bool allowFailure = false) {
        string summary = "";

        summary += SummarizeTrigger(effect.trigger);
        summary += SummarizeTarget(effect.target);
        summary += SummarizePrimaryTargetSelectors(effect, allowFailure);

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

    private static string SummarizePrimaryTargetSelectors(Effect effect, bool allowFailure = false) {
        Debug.Log(effect.target.type.ToString());
        switch (effect.target.type) {
            case Effect.Target.Type.TARGET_ATTRIBUTE:
                List<TargetAttributeArguments> targetArguments = effect.target.arguments.ToList().Select(argumentList => {
                    return new TargetAttributeArguments() {
                        name = Enum.GetName(typeof(Officer.Attribute), argumentList[0]),
                        absoluteChange = argumentList[1],
                        percentageChange = argumentList[2]
                    };
                }).ToList();

                string initial = "Modify " + (targetArguments.Count > 1 ? "attributes" : "attribute") + ":\n";
                string finalString = "";

                targetArguments.ForEach(argumentStruct => {
                    bool isAbsolute = argumentStruct.absoluteChange != 0 ? true : false;
                    bool isRelative = argumentStruct.percentageChange != 0 ? true : false;

                    if (!isAbsolute && !isRelative) {
                        if (allowFailure) {
                            return;
                        }
                        else {
                            throw new Exception("The effect of target attribute is neither absolute or relative, please check the effect of id " + effect.id);
                        }
                    }

                    finalString += argumentStruct.name + " by " + (isAbsolute ? argumentStruct.absoluteChange.ToString() : argumentStruct.percentageChange.ToString() + "%") + "\n";
                });

                if (finalString == "") {
                    return "";
                }
                else {
                    return initial + finalString;
                }

            default:
                return "TARGET TYPE " + effect.target.type + " NOT FOUND IN ENUMERATOR";
        }
    }

    public struct TargetAttributeArguments
    {
        public string name;
        public int absoluteChange;
        public float percentageChange;
    }
}