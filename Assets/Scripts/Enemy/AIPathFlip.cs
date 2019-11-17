using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class AIPathFlip : MonoBehaviour
{

    protected IAstarAI pathfindingAgent;

    private void Start()
    {
        pathfindingAgent = GetComponent<IAstarAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pathfindingAgent.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        } else if (pathfindingAgent.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
