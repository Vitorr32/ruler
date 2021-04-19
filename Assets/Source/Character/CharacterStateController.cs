using System.Collections.Generic;

public class CharacterStateController
{
    public Character baseCharacter;

    public List<OfficerSprite> charSprites = new List<OfficerSprite>();
    public List<Trait> serializedTraits = new List<Trait>();

    // Accesible and modifiable state of the character
    public int currentMood;
    public int currentStress;
    public int currentEnergy;

    //Passive dynamic values of the character, these are values that affect the character behaviour and other attributes.
    public float moodGainModifier = 1.0f;
    public float stressGainModifier = 1.0f;
    public float energyGainModifier = 1.0f;

    public CharacterStateController(Character character) {
        this.currentEnergy = character.baseEnergy;
        this.currentMood = character.baseMood;
        this.currentEnergy = character.baseEnergy;

        this.baseCharacter = character;
    }
}
