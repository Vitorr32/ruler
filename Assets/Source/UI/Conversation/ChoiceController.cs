using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct ChoiceRestrition
{
    public enum Type
    {
        HAS_ITEM,
        DONT_HAVE_ITEM,
        RELATIONSHIP_TYPE,
        ATTRIBUTE_HIGHER_THAN,
        ATTRIBUTE_LOWER_THAN,
        HAS_EFFECT,
        HAS_TRAIT
    }

    public Type type;
    public int[] arguments;
}

public struct ChoiceFeedback
{
    public enum Type
    {
        SMALL_TALK,
        TALK,
        TALK_ABOUT_LOCATION,
        TALK_ABOUT_HOBBY,
        TALK_ABOUT_WORK,
        GOSSIP
    }

    public Type type;
    public int[] arguments;
}

public struct Choice
{
    public string line;

    ChoiceRestrition restriction;
    ChoiceFeedback feedback;

    //If the choice takes you to a specific path on the conversation
    int pathID;
}
public class ChoiceController : MonoBehaviour
{
    public delegate void OnChoiceSelected(Choice choice);
    public static event OnChoiceSelected onChoiceSelected;

    // Start is called before the first frame update
    public GameObject choicePrefab;

    public void ShowChoices(List<Choice> choices) {
        gameObject.SetActive(true);

        choices.ForEach(choice => {
            GameObject instantiatedChoice = Instantiate(choicePrefab, transform);

            instantiatedChoice.GetComponentInChildren<Text>().text = choice.line;
        });
    }
}
