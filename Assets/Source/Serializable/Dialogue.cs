using System;
using System.Collections.Generic;

public enum LinePooling
{
    UNDEFINED,

    RANDOM_POOLING,
    FIRST_POOLING,

    MAX_LINE_POOLING
}
[Serializable]
public class Dialogue
{
    [Serializable]
    //How much a specific trait has a tendency of chosing this specific dialogue
    public struct TraitPush
    {
        public int traitID;
        public float weight;
    }

    public struct ChoiceOption
    {
        //What is the dialogue that this option will trigger
        public string nextDialogueID;
        //What is the condition for this option to be active, if null it will always be active
        public ConditionTree condition;
        //What is the label that will appear in the option button
        public string label;
    }

    public List<Scene> lines;
    public LinePooling linePoolingMethod;

    //If the dialogue will need the player to input something such as an value or select an character
    public bool hasInput;

    public List<string> nextDialogueID;

    public Condition condition;
}
