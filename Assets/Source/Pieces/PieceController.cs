using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PieceController : MonoBehaviour, IPointerClickHandler
{
    public delegate void OnPieceClick(OfficerController officer);
    public static event OnPieceClick onPieceClicked;

    public delegate void OnPieceCollision(OfficerController mainPiece, OfficerController targetPiece);
    public static event OnPieceCollision onPieceCollided;

    public OfficerController associatedOfficer;
    public Vector3 currentDestination;
    private NavMeshAgent navMeshAgent;
    private TickController.Speed gameSpeed;

    // Start is called before the first frame update
    void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();

        gameSpeed = TickController.instance.currentSpeed;
        navMeshAgent.speed = TickController.instance.getTickSpeed(gameSpeed);

        TickController.onTimeAdvance += onDateChange;
    }

    private void OnDestroy() {
        TickController.onTimeAdvance -= onDateChange;
    }

    // Update is called once per frame
    void Update() {
        if (navMeshAgent.destination != currentDestination) {
            navMeshAgent.SetDestination(currentDestination);
        }
    }

    void onDateChange(DateTime currentTime, TickController.Speed newSpeed) {
        gameSpeed = newSpeed;
        if (gameSpeed == TickController.Speed.PAUSED) {
            navMeshAgent.isStopped = true;
        }
        else {
            navMeshAgent.isStopped = false;
            navMeshAgent.speed = TickController.instance.getTickSpeed(gameSpeed);
        }
    }

    public void ChangeActorDestination(Vector3 vector) {
        currentDestination = vector;
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left) {
            onPieceClicked?.Invoke(GetComponent<OfficerController>());
        }
    }

    public void OnTriggerEnter(Collider other) {
        if (other.tag != "Interactable") { return; }

        PieceController triggeredPiece = other.GetComponent<PieceController>();

        if (!triggeredPiece) {
            return;
        }
        else {
            onPieceCollided?.Invoke(associatedOfficer, triggeredPiece.associatedOfficer);
        }
    }
}
