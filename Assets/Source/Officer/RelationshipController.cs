using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Opinion
{
    UNKNOWN,
    ACQUAINTANCE,
    FRIEND,
    GOOD_FRIEND,
    DEPENDENT_FREND,
    LEADING_FRIEND,
    LONG_DATE_FRIEND,
    BROTHERHOOD,
    DISTRUSTFUL,
    PARANOIC_DISTRUST,
    DISLIKE,
    HATE,
    BULLY,
    BULLIED,
    CRUSH,
    CARNAL_DESIRE,
    SEX_FRIEND,
    LOVE,
    TRUE_LOVE,
    SLAVE,
    OWNER,
    UNWILLING_SLAVE,
    DREADFUL_MASTER,
    SERVANT,
    MASTER,
    BENEVOLENT_MASTER,
    TRUSTWORTH_SERVANT
}


[Serializable]
public struct Relationship
{
    public int desire;
    public int trust;
    public int dominance;
    public int knowledge;
    public Opinion opinion;

    //Check the date where the relationship was estabilshed, also if the Officer know the appearance of the target officer
    public string establishedDate;
    public string lastInteractionDate;
    public bool knowAppearance;

    public int targetID;
}

public static class RelationshipController
{

    public static bool DoesOfficerRecognizeOther(Relationship o1, Relationship o2) {
        return o1.knowAppearance && o2.knowAppearance;
    }

    public static bool DoesOfficerMeetOther(Relationship o1, Relationship o2) {
        return o1.establishedDate != null && o2.establishedDate != null;
    }
}
