
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using System.Runtime.CompilerServices;

public static class Summarizer
{
    public static string SummarizeEffect(Effect effect) {
        string summary = "";

        summary += SummarizeTrigger(effect.trigger);
        summary += SummarizeConditionTree(effect.conditionTree);
        summary += SummarizeModifierTargets(effect.modifier);

        return summary;
    }

    private static string SummarizeTrigger(Effect.Trigger trigger) {
        switch (trigger) {
            case Effect.Trigger.ALWAYS_ACTIVE:
                return "Always: " + Environment.NewLine;
            case Effect.Trigger.ON_INTERACTION_START:
                return "When interaction starts:  " + Environment.NewLine;
            case Effect.Trigger.INTERACTION_END:
                return "When interaction ends:" + Environment.NewLine;
            default:
                return "";
        }
    }

    public static string SummarizeConditionTree(ConditionTree conditionTree) {
        string finalString = "";

        if (conditionTree != null && conditionTree.root != null) {
            finalString += SummarizeConditionNode(conditionTree.root);
        }

        return finalString;
    }

    private static string SummarizeConditionNode(ConditionTree.Node conditionNode, int layer = 0) {
        string conditionString = "";

        switch (conditionNode.logicOperator) {
            case LogicOperator.IF:
                conditionString += "If the following is true:" + Environment.NewLine;
                break;
            case LogicOperator.AND:
                conditionString += "If all of the following is true: " + Environment.NewLine;
                break;
            case LogicOperator.OR:
                conditionString += "If any of the following is true: " + Environment.NewLine;
                break;
            default:
                conditionString += "LOGIC_OPERATOR";
                break;
        }

        conditionNode.conditions.ForEach(condition => {
            switch (condition.initiator) {
                case Condition.Initiator.ATTRIBUTE_RANGE:
                    conditionString += "Attribute ";
                    if (condition.attrRange.attrRangeParameters == null || condition.attrRange.attrRangeParameters.Count() == 0) {
                        return;
                    }
                    int[] attrParams = condition.attrRange.attrRangeParameters;

                    Attribute attribute = StoreController.instance.FindAttribute(condition.attrRange.attrRangeParameters[0]);
                    conditionString += attribute.name;

                    conditionString += " of " + condition.agent.ToString();

                    int firstNumberAttr = attrParams.Count() > 1 ? attrParams[1] : -1;
                    int secondNumberAttr = attrParams.Count() > 2 ? attrParams[2] : -1;

                    conditionString += EnumToString.GetStringOfConditionNumericSelectorValues(
                        condition.attrRange.selector,
                        firstNumberAttr, secondNumberAttr
                    );

                    break;
                case Condition.Initiator.STATUS_RANGE:
                    conditionString += condition.statusRange.statusRangeParameters;
                    int[] statusParams = condition.attrRange.attrRangeParameters;

                    if (statusParams == null || statusParams.Count() == 0) {
                        return;
                    }

                    conditionString += (Character.Status)statusParams[0];
                    int firstNumberStatus = statusParams.Count() > 1 ? statusParams[1] : -1;
                    int secondNumberStatus = statusParams.Count() > 2 ? statusParams[2] : -1;

                    conditionString += EnumToString.GetStringOfConditionNumericSelectorValues(
                        condition.statusRange.selector,
                        firstNumberStatus, secondNumberStatus
                    );

                    break;
            }
        });
        return conditionString;
    }

    private static string SummarizeModifierTargets(Modifier modifier) {
        if (modifier.type == Modifier.Type.UNDEFINED) {
            Debug.LogError("The modifier has no targets, please check the effect");
            return "";
        }

        switch (modifier.type) {
            case Modifier.Type.MODIFY_ATTRIBUTE_VALUE:
                string initial = "Modify " + (modifier.modifierTargets.Count > 1 ? "attributes" : "attribute") + Environment.NewLine;
                string finalString = "";

                foreach (int modifierTarget in modifier.modifierTargets) {
                    Attribute skill = StoreController.instance.attributes.Find(skill => skill.id == modifierTarget);
                    finalString += "    " + skill.name + " by ";
                    finalString += modifier.effectiveChange == 0
                        ? "X"
                        : modifier.effectiveChange.ToString();
                    finalString += Environment.NewLine;
                }

                return initial + finalString;
            default:
                return "TARGET TYPE " + modifier.type + " NOT FOUND IN ENUMERATOR";
        }
    }

    public struct TargetAttributeArguments
    {
        public string name;
        public int absoluteChange;
        public float percentageChange;
    }
}