
using System.Collections.Generic;

public class Skill
{
    public enum Category
    {
        UNDEFINED,

        BASIC,
        SUNLIGHT,
        STARLIGHT,
        MOONLIGHT,

        MAX_CATEGORIES
    }

    public enum Growth
    {
        UNDEFINED,

        // Grow with training, decrease with lack of use
        TECHINAL,
        // Grow with age and emotial stability, decrease with emotional instability. Starts low with young Characters
        MENTAL,
        // Curved growth: increases with age until peak, then slow decreases until retirement
        PHYSICAL,

        MAX_GROWTHS
    }

    public Category category = Category.BASIC;
    public Growth growth = Growth.TECHINAL;

    //Description that will appear when the player hover over the skill
    public string description;
    //Encoding used to show skill name in interface
    public string name;
    //Identifier to find the skill on search or serialization of JSON
    public int id;

}
