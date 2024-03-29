﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    [Header("Layers")] public LayerMask groundLayer;
    public LayerMask platformsLayer;

    [Space] public bool onGround;
    public bool onWall;
    public bool onRightWall;
    public bool onLeftWall;
    public bool roofCollision;
    public int wallSide;

    [Space] [Header("Collision")] public float collisionRadius = 0.25f;
    public Vector2 bottomOffset, rightOffset, leftOffset, upperOffset;
    private Color debugCollisionColor = Color.red;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.OverlapCircle((Vector2) transform.position + bottomOffset, collisionRadius, groundLayer)
                   || Physics2D.OverlapCircle((Vector2) transform.position + bottomOffset, collisionRadius,
                       platformsLayer);

        onWall = Physics2D.OverlapCircle((Vector2) transform.position + rightOffset, collisionRadius, groundLayer)
                 || Physics2D.OverlapCircle((Vector2) transform.position + leftOffset, collisionRadius, groundLayer);

        onRightWall = Physics2D.OverlapCircle((Vector2) transform.position + rightOffset, collisionRadius, groundLayer);
        onLeftWall = Physics2D.OverlapCircle((Vector2) transform.position + leftOffset, collisionRadius, groundLayer);

        roofCollision =
            Physics2D.OverlapCircle((Vector2) transform.position + upperOffset, collisionRadius, groundLayer);

        wallSide = onRightWall ? -1 : 1;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        var positions = new Vector2[] {bottomOffset, rightOffset, leftOffset};

        Gizmos.DrawWireSphere((Vector2) transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2) transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2) transform.position + leftOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2) transform.position + upperOffset, collisionRadius);
    }
}