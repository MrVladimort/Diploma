using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class DemoPlayer : LiveThing
{
    [FormerlySerializedAs("MaxSpeed")] public float maxSpeed = 5f;
    [FormerlySerializedAs("JumpTakeOffSpeed")] public float jumpTakeOffSpeed = 7f;
    [FormerlySerializedAs("SwordAnimatorOverrideController")] public AnimatorOverrideController swordAnimatorOverrideController;
    
    private bool _isSliding;
    private bool _haveSword;
    
    private static readonly int IsGrounded = Animator.StringToHash("isGrounded");
    private static readonly int HaveSword = Animator.StringToHash("haveSword");
    private static readonly int IsSliding = Animator.StringToHash("isSliding");
    private static readonly int RunVelocity = Animator.StringToHash("runVelocity");
    private static readonly int Attack = Animator.StringToHash("attack");

    // Use this for initialization
    protected override void Start()
    {        
        base.Start();
        CheckGameMasterOptions();
    }

    public void TakeSword()
    {
        _haveSword = true;
        Animator.runtimeAnimatorController = swordAnimatorOverrideController;
        GameMaster.haveSword = _haveSword;
    }

    private void CheckGameMasterOptions()
    {
        transform.position = GameMaster.GetCheckPoint();
        if (GameMaster.haveSword) TakeSword();
    }

    protected override void ComputeVelocity()
    {
        base.ComputeVelocity();
        
        if (!IsDead) CalculateInputs();
    
        SetAnimatorParametres();
        TargetVelocity = Move * maxSpeed;
    }

    private void CalculateInputs()
    {
        Move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && Grounded) Velocity.y = jumpTakeOffSpeed;
        else if (Input.GetButtonUp("Jump"))
        {
            if (Velocity.y > 0) Velocity.y = Velocity.y * 0.5f;
        }

        if (Input.GetButtonDown("Slide"))
        {
            _isSliding = true;
        }
        else if (Input.GetButtonUp("Slide"))
        {
            _isSliding = false;
        }

  
        if (Input.GetButtonDown("Attack") && _haveSword)
        {
            Animator.SetTrigger(Attack);
        }
    }

    private void SetAnimatorParametres()
    {
        Animator.SetBool(IsGrounded, Grounded);
        Animator.SetBool(HaveSword, _haveSword);
        Animator.SetBool(IsSliding, _isSliding);
        Animator.SetFloat(RunVelocity, Mathf.Abs(Velocity.x) / maxSpeed);
    }

    protected override IEnumerator Respawn()
    {
        yield return new WaitForSecondsRealtime(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}