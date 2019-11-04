using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum PlayerState {
    Idle,
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
        if (Input.GetButton("Slide") && move.coll.onGround)
        {
            slide = true;
        }
    }

    private IEnumerator AttackCo()
    {
        var previousState = currentState;
        anim.SetTrigger(AttackAnimatorMapping);
        currentState = PlayerState.Attack;
        yield return new WaitForSeconds(0.8f);
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