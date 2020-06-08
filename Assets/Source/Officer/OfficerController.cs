using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class OfficerController
{
    public Officer baseOfficer;
    public PieceController piece;

    public List<Sprite> officerSprite;
    public List<Trait> serializedTraits = new List<Trait>();
    public List<Effect> activeEffects = new List<Effect>();

    public bool onOverworld;
    public bool isPlayer;

    public void StartUpController(Officer officer) {
        baseOfficer = officer;

        serializedTraits = GetOfficerTraitsFromStore(baseOfficer);
        activeEffects = StartUpEffectsOnOfficer(serializedTraits);
    }

    public int GetOfficerID() {
        return baseOfficer.id;
    }

    private List<Trait> GetOfficerTraitsFromStore(Officer officer) {
        List<Trait> traits = new List<Trait>();

        officer.traits.ForEach((int traitID) => {
            Trait trait = StoreController.instance.traits.FirstOrDefault(aTrait => aTrait.id == traitID);

            if (trait == null) {
                Debug.Log("Trait of ID: " + traitID + " does not exist!");
            }

            traits.Add(trait);
        });

        return traits;
    }

    private List<Effect> StartUpEffectsOnOfficer(List<Trait> traits) {
        List<Effect> effects = new List<Effect>();

        traits.ForEach(trait => {
            trait.uEffects.ForEach(effect => {
                if (ShouldEffectActivate(baseOfficer, trait, effect, ActionType.ALWAYS_ACTIVE)) {
                    activeEffects.Add(effect);
                }
            });
        });

        return effects;
    }
    //Static method that verifies if the target should receive a effect deriving from source T during a certaing action
    public static bool ShouldEffectActivate<T>(Officer target, T source, Effect effect, ActionType triggerType) {
        if (effect.trigger.type != triggerType) { return false; }

        switch (triggerType) {
            case ActionType.NO_TRIGGER:
            case ActionType.ALWAYS_ACTIVE:
                return true;
            default:
                Debug.Log("Unknown action of type " + triggerType);
                return false;
        }
    }
}
