using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float chasingDistance = 10;
    
    protected Rigidbody2D rb;
    protected bool seePlayer;
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
        
    }
    
    
}
