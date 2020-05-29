using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControlller : MonoBehaviour
{
    public PieceController playerActor = null;
    public Terrain gameTerrain = null;

    // Start is called before the first frame update
    void Start() {
        TerrainController.onTerrainClicked += onMapClicked;
    }

    private void OnDestroy() {
        TerrainController.onTerrainClicked -= onMapClicked;
    }

    private void onMapClicked(Vector3 worldPoint) {
        playerActor.ChangeActorDestination(worldPoint);
    }

    // Update is called once per frame
    void Update() {
    }
}
