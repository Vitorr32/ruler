using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Emotion that actor will show during the line
public enum Emotion
{
    NEUTRAL,
    HAPPY,
    SAD,
    ANGRY,
    FURIOUS,
    SUSPICIOUS,
    SCARED
}
public class Scene
{

    public struct Sound
    {
        public string soundSource;
        public int soundTimeTrigger;
    }

    public struct Animation
    {

    }

    public string backgroundSource;
    public int id;
    //Automatic populated when the scene is loaded
    public List<CharacterController> actors;

    public string dialog;
}
