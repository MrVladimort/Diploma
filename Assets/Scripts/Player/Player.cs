using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum PlayerState {
    Idle,
    Slide,
    Walk,
    Run,
    Jump,
    Attack,
    Interact,
    Hurt,
    Dash,
    WallGrab,
    WallSlide
}

public class Player : MonoBehaviour
{
    public PlayerState currentState;
    
    [HideInInspector] public Rigidbody2D rb;
    private AnimationScript anim;
    private Movement move;

    [Space] [Header("TriggersValue")]
    private static readonly int AttackAnimatorMapping = Animator.StringToHash("attack");

    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.Idle;
        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<AnimationScript>();
        move = GetComponent<Movement>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Attack") && move.coll.onGround && currentState != PlayerState.Attack)
        {
            StartCoroutine(AttackCo());
        }
    }

    private IEnumerator AttackCo()
    {
        var previousState = currentState;
        move.canMove = false;
        currentState = PlayerState.Attack;
        anim.SetTrigger(AttackAnimatorMapping);
        yield return new WaitForSeconds(1f);
        move.canMove = true;
        currentState = previousState;
    }
    
    public void Knock(Rigidbody2D myRigidBody, float knockTime)
    {
        StartCoroutine(KnockCo(myRigidBody, knockTime));
    }

    private IEnumerator KnockCo(Rigidbody2D myRigidBody, float knockTime)
    {
        if (myRigidBody != null && currentState == PlayerState.Hurt)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidBody.velocity = Vector2.zero;
            currentState = PlayerState.Idle;
        }
    }
}