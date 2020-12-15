using System.Collections.Generic;

public enum Condition
{
    CHARACTER_STATE_IS,
    CHARACTER_HAS,
    TIME_IS,
    INTERACTING_WITH
}

public enum CharacterState
{
    TALKING,
    DEAD,
    AT
}

public enum CharacterHas
{
    RELATIONSHIP
}

public static class ConditionMapper
{
    //public static Dictionary<int, List<int>> triggerToTargetDictionary = new Dictionary<int, List<int>>() {
    //    {
    //        (int)Condition.CHARACTER_IS,
    //        new List<int>(){
    //            (int)Effect.Target.Type.TARGET_ATTRIBUTE,
    //            (int)Effect.Target.Type.TARGET_POPULARITY_GAIN,
    //            (int)Effect.Target.Type.TARGET_STRESS_GAIN
    //        }
    //    }
    //};

}
