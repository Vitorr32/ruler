using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PieceController : MonoBehaviour, IPointerClickHandler
{
    public delegate void OnActorClick(OfficerController officer);
    public static event OnActorClick onActorClicked;

    public OfficerController associatedOfficer;
    private bool isPlayer = false;
    public Vector3 currentDestination;

    private NavMeshAgent navMeshAgent;

    private TickController.Speed gameSpeed;
    private float trueSpeed;

    private float tick = 50;

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
            onActorClicked?.Invoke(associatedOfficer);
        }
    }
}
