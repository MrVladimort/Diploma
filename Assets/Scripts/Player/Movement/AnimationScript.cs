using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{

    private Animator anim;
    private Movement move;
    private Collision coll;
    [HideInInspector]
    public SpriteRenderer sr;

    [Space] [Header("TriggersValue")]
    private static readonly int Slide = Animator.StringToHash("sliding");
    private static readonly int IsDashing = Animator.StringToHash("isDashing");
    private static readonly int CanMove = Animator.StringToHash("canMove");
    private static readonly int WallSlide = Animator.StringToHash("wallSlide");
    private static readonly int WallGrab = Animator.StringToHash("wallGrab");
    private static readonly int OnRightWall = Animator.StringToHash("onRightWall");
    private static readonly int OnWall = Animator.StringToHash("onWall");
    private static readonly int OnGround = Animator.StringToHash("onGround");
    private static readonly int HorizontalAxis = Animator.StringToHash("HorizontalAxis");
    private static readonly int VerticalAxis = Animator.StringToHash("VerticalAxis");
    private static readonly int VerticalVelocity = Animator.StringToHash("VerticalVelocity");

    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponentInParent<Collision>();
        move = GetComponentInParent<Movement>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        anim.SetBool(OnGround, coll.onGround);
        anim.SetBool(OnWall, coll.onWall);
        anim.SetBool(OnRightWall, coll.onRightWall);
        anim.SetBool(WallGrab, move.wallGrab);
        anim.SetBool(WallSlide, move.wallSlide);
        anim.SetBool(CanMove, move.canMove);
        anim.SetBool(IsDashing, move.isDashing);
        anim.SetBool(Slide, move.slide);
    }

    public void SetHorizontalMovement(float x,float y, float yVel)
    {
        anim.SetFloat(HorizontalAxis, x);
        anim.SetFloat(VerticalAxis, y);
        anim.SetFloat(VerticalVelocity, yVel);
    }

    public void SetTrigger(int trigger)
    {
        anim.SetTrigger(trigger);
    }

    public void Flip(int side)
    {

        if (move.wallGrab || move.wallSlide)
        {
            if (side == -1 && sr.flipX)
                return;

            if (side == 1 && !sr.flipX)
            {
                return;
            }
        }

        bool state = side != 1;
        sr.flipX = state;
    }
}
