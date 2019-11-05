﻿using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public float MinGroundNormalY = .65f;
    public float GravityModifier = 1f;
    public Transform WallTrigger;
    public float Range = 0.7f;
    public bool FreezeY = false;

    protected Vector2 Velocity;
    protected Vector2 TargetVelocity;
    protected Vector2 GroundNormal;
    protected Vector2 Move;
    protected bool Grounded;
    
    protected Rigidbody2D Rb2D;
    protected Animator Animator;
    protected SpriteRenderer SpriteRenderer;
    protected GameMaster GameMaster;
    protected Vector3 Direction = new Vector3(-1, 0, 0);

    private ContactFilter2D _contactFilter;
    private readonly RaycastHit2D[] _hitBuffer = new RaycastHit2D[16];
    private readonly List<RaycastHit2D> _hitBufferList = new List<RaycastHit2D>(16);
    private const float MinMoveDistance = 0.001f;
    private const float ShellRadius = 0.01f;

    private bool _isFacingRight;

    protected virtual void Start()
    {
        _isFacingRight = true;

        _contactFilter.useTriggers = false;
        _contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        _contactFilter.useLayerMask = true;
        
        Rb2D = GetComponent<Rigidbody2D>();
        GameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        TargetVelocity = Vector2.zero;
        ComputeVelocity();
    }

    protected virtual void ComputeVelocity()
    {
        Move = Vector2.zero;
    }

    void FixedUpdate()
    {
        if (CheckWallTrigger()) TargetVelocity = Vector2.zero;
        
        Velocity += GravityModifier * Physics2D.gravity * Time.deltaTime;
        Velocity.x = TargetVelocity.x;

        Grounded = false;

        Vector2 deltaPosition = Velocity * Time.deltaTime;

        Vector2 moveAlongGround = new Vector2(GroundNormal.y, -GroundNormal.x);

        Vector2 move = moveAlongGround * deltaPosition.x;

        Flip(move);

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
    }

    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > MinMoveDistance)
        {
            int count = Rb2D.Cast(move, _contactFilter, _hitBuffer, distance + ShellRadius);
            _hitBufferList.Clear();
            for (int i = 0; i < count; i++)
            {
                _hitBufferList.Add(_hitBuffer[i]);
            }

            for (int i = 0; i < _hitBufferList.Count; i++)
            {
                Vector2 currentNormal = _hitBufferList[i].normal;

                if (currentNormal.y > MinGroundNormalY)
                {
                    Grounded = true;
                    if (yMovement)
                    {
                        GroundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(Velocity, currentNormal);
                if (projection < 0)
                {
                    Velocity = Velocity - projection * currentNormal;
                }

                float modifiedDistance = _hitBufferList[i].distance - ShellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        if (FreezeY) move.y = 0;
        Rb2D.position = Rb2D.position + move.normalized * distance;
    }

    private void Flip(Vector2 move)
    {
        if (move.x < -0.01f && !_isFacingRight) FlipGameObject();
        else if (move.x > 0.01f && _isFacingRight) FlipGameObject();
    }

    private void FlipGameObject()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 mirrorScale = gameObject.transform.localScale;
        mirrorScale.x *= -1;
        gameObject.transform.localScale = mirrorScale;
    }

    protected bool CheckWallTrigger()
    {
        Debug.DrawRay(WallTrigger.position, new Vector3(transform.localScale.x, 0, 0) * Range);
        if (IsNotSameDirection(TargetVelocity.x, transform.localScale.x)) return false;
        
        RaycastHit2D wallHit =
            Physics2D.Raycast(WallTrigger.position, new Vector3(transform.localScale.x, 0, 0), Range);

        if (wallHit == true)
            if (wallHit.collider.CompareTag("Ground"))
                return true;

        return false;
    }
    
    protected void ChangeDirection()
    {
        Direction *= -1;
    }

    private static bool IsNotSameDirection(float a, float b)
    {
        return !(a < 0 && b < 0 || a > 0 && b > 0);
    }
}
