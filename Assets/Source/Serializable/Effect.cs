using System.Collections;
using System.Collections.Generic;

public enum Trigger
{
    NO_TRIGGER,
    ON_ATTACK,
    ON_DEFENSE,
    ON_ENTERING,
    ON_LEAVING,
    ON_TILE_TYPE,
    ON_OVERWORLD,
    ON_DIALOGUE
}

public class Effect
{
    Effect(int pID, Trigger[] pTriggers) {
        id = pID;
        triggers = pTriggers;
    }

    private int id;
    private Trigger[] triggers;


}
