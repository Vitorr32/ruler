﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecesController : MonoBehaviour
{
    private StoreController store;

    public Terrain terrain;
    public GameObject officerPrefab;
    private void Awake() {
        store = FindObjectOfType<StoreController>();

        SetPiecesOnBoard();
    }

    void SetPiecesOnBoard() {        
        foreach (OfficerController controller in store.officers) {
            int[] position = controller.baseOfficer.position;

            Vector3 officerPosition = new Vector3(position[0], 0, position[1]);

            float heightOnTerrain = terrain.SampleHeight(officerPosition);

            officerPosition.y = heightOnTerrain;

            GameObject actorOfOfficer = Instantiate(officerPrefab, officerPosition, Quaternion.identity);

            actorOfOfficer.GetComponent<ActorController>().associatedOfficer = controller;
        }
    }
}
