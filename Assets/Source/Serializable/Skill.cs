
public class Skill
{
    public enum Type
    {
        UNDEFINED,

        ABSOLUTE_VALUE,
        CATEGORY_VALUE,

        MAX_TYPES
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

    public string category;
    public Type type = Type.ABSOLUTE_VALUE;
    public Growth growth = Growth.TECHINAL;

    public int maxValue = 100;
    public int minValue = 0;

    //Description that will appear when the player hover over the skill
    public string description;
    //Encoding used to show skill name in interface
    public string displayName;

    public int id;

}
