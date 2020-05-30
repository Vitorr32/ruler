using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationController : MonoBehaviour
{
    public GameObject overlay;
    public GameObject background;

    // Start is called before the first frame update
    void Start() {
        PieceController.onPieceCollided += OnPlayerTriggeredConversation;
    }

    void OnDestroy() {
        PieceController.onPieceCollided -= OnPlayerTriggeredConversation;
    }

    // Update is called once per frame
    void Update() {

    }

    private void SetUpConversationBetweenOfficers(OfficerController source, OfficerController target) {
        Relationship[] relationships = GetRelationshipBetweenCharacters(source.baseOfficer, target.baseOfficer);


    }

    private Relationship[] GetRelationshipBetweenCharacters(Officer officer1, Officer officer2) {
        Relationship[] relationships = new Relationship[2];

        relationships[0] = officer1.relationships.Find(relationship => relationship.targetID == officer2.id);
        relationships[1] = officer2.relationships.Find(relationship => relationship.targetID == officer1.id);

        return relationships;
    }

    private void OnPlayerTriggeredConversation(OfficerController source, OfficerController target) {
        if (!source.isPlayer) {
            return;
        }

        gameObject.SetActive(true);
    }
}
