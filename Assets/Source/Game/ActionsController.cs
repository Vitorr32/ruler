using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType
{
    UNDEFINED,

    ALWAYS_ACTIVE,
    ON_ATTACK,
    ON_DEFENSE,
    ON_ENTERING,
    ON_LEAVING,
    ON_TILE_TYPE,
    ON_OVERWORLD,
    ON_DIALOGUE,
    ON_DAY_CHANGE,
    ON_HOUR_CHANGE,
    ON_ATTRIBUTE_CHANGE,

    MAX_ACTION_TYPES
}
public class ActionsController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
