using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public enum LogicOperator
{
    IF,
    AND,
    OR
}
public class ConditionTree
{
    public class Node
    {
        //The logic operator of this node, will define how the evaluation of the nodes conditions/children will be evaluated
        public LogicOperator logicOperator = LogicOperator.IF;
        //The child nodes of this node, for this node evaluation to be true the children also neeed to be true
        public List<Node> children = new List<Node>();
        //The list of conditions of this specific node, together with the logic operator, will define the evaluation of this node
        public List<Condition> conditions = new List<Condition>();

        /*
        public bool EvaluateNode() {
            //The children Values don't matter if the node condition is already a false
            if (!condition.EvaluateCondition(null, null)) {
                return false;
            }

            //Check if the OR and AND logic is nescessary, if not just do a simple check for all children conditions evalutation
            if (children.Exists(Node => Node.condition.logicOperator == Condition.LogicOperator.OR
                                     || Node.condition.logicOperator == Condition.LogicOperator.AND)) {
                this.GroupByDependentConditions(this.children);
                return true;
            }
            else {
                List<bool> childrenEvaluation = children.Select(child => child.EvaluateNode()).ToList();
                //If any of the simples evaluation is false, return just false, else return true if all are true
                return !childrenEvaluation.Exists(evaluation => !evaluation);
            }
        }

        //Separate the list of childen conditions into chunks of interdependent logic bits, for example an if + and
        //Tha both need to be true to the overal combination to return true.
        public List<List<Node>> GroupByDependentConditions(List<Node> nodes) {
            List<List<Node>> dependentConditions = new List<List<Node>>();

            Node previousNode = null;

            nodes.ForEach(node => {
                //If clean iteration without previous dependent condition
                if (previousNode == null) {
                    //Check if the new node is correctly assigned the IF logic operator
                    if (previousNode.condition.logicOperator != Condition.LogicOperator.IF) {
                        Debug.LogError("The condition after the previous condition group is not assigned the correct operator IF");
                        return;
                    }
                    //Set the previous condition
                    previousNode = node;
                    // Create a new list for the current condition inter-dependency
                    dependentConditions.Add(new List<Node>() { node });
                    return;
                }

                //Either add the current node to a new group or add to the last grouping 
                switch (node.condition.logicOperator) {
                    case Condition.LogicOperator.IF:
                        dependentConditions.Add(new List<Node>() { node });
                        break;
                    case Condition.LogicOperator.AND:
                    case Condition.LogicOperator.OR:
                        dependentConditions[dependentConditions.Count - 1].Add(node);
                        break;
                    default:
                        Debug.LogError("Node in the condition tree has no logic operator assigned");
                        break;
                }

                previousNode = node;
            });

            return dependentConditions;
        }
        */

    }
    public Node root;

    public bool EvaluateConditionTree() {
        return true;
        //return root.EvaluateNode();
    }
}
