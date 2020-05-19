using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActorController : MonoBehaviour
{
    private bool isPlayer = false;
    public Vector3 currentDestination;
    private NavMeshAgent navMeshAgent;

    private float tick = 50;

    // Start is called before the first frame update
    void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentDestination = new Vector3(358.0355f, 89.1f, 1065.375f);
        navMeshAgent.SetDestination(currentDestination);
    }

    // Update is called once per frame
    void Update() {
        if (navMeshAgent.destination != currentDestination) {
            navMeshAgent.SetDestination(currentDestination);
        }

        tick -= Time.deltaTime;
        if (tick <= 0) {
            tick = 5000;
            currentDestination = new Vector3(Random.Range(0, 1000), 0, Random.Range(0, 1000));
        }
    }
}
