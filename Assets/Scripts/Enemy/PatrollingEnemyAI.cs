using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PatrollingEnemyAI : MonoBehaviour
{
    public Transform[] patrollingPoints;
    public float patrolDelay = 0;
    
    int patrollingIndex;
    float switchTime = float.PositiveInfinity;

    public float chasingDistance = 10f;

    protected Rigidbody2D rb;
    protected Transform target;
    protected IAstarAI pathfindingAgent;
    protected Vector3 spawnPoint;
    protected Enemy enemy;
    
    // Start is called before the first frame update
    protected void Start()
    {
        spawnPoint = transform.position;
        
        rb = GetComponent<Rigidbody2D>();
        pathfindingAgent = GetComponent<IAstarAI>();
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemy.seePlayer)
        {
            Patrolling();
        } 
    }

    void Patrolling()
    {
        if (patrollingPoints.Length == 0) return;

        bool search = false;

        // Note: using reachedEndOfPath and pathPending instead of reachedDestination here because
        // if the destination cannot be reached by the agent, we don't want it to get stuck, we just want it to get as close as possible and then move on.
        if (pathfindingAgent.reachedEndOfPath && !pathfindingAgent.pathPending && float.IsPositiveInfinity(switchTime)) {
            switchTime = Time.time + patrolDelay;
        }

        if (Time.time >= switchTime) {
            patrollingIndex = patrollingIndex + 1;
            search = true;
            switchTime = float.PositiveInfinity;
        }

        patrollingIndex = patrollingIndex % patrollingPoints.Length;
        pathfindingAgent.destination = patrollingPoints[patrollingIndex].position;

        if (search)
        {
            enemy.currentState = EnemyState.Patrolling;
            pathfindingAgent.SearchPath();
        }
        else
        {
            enemy.currentState = EnemyState.Idle;
        }
    }
}