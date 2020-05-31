using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecesController : MonoBehaviour
{
    public Terrain terrain;
    public GameObject officerPrefab;
    private void Awake() {
        SetPiecesOnBoard();
    }

    private void SetPiecesOnBoard() {
        foreach (OfficerController controller in StoreController.instance.officers) {
            int[] position = controller.baseOfficer.position;

            Vector3 officerPosition = new Vector3(position[0], 0, position[1]);

            float heightOnTerrain = terrain.SampleHeight(officerPosition);

            officerPosition.y = heightOnTerrain;

            GameObject actorOfOfficer = Instantiate(officerPrefab, officerPosition, Quaternion.identity);

            actorOfOfficer.GetComponent<PieceController>().associatedOfficer = controller;
            controller.piece = actorOfOfficer.GetComponent<PieceController>();
        }
    }
}
