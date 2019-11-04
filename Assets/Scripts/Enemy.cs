using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Run,
    Attack,
    Hurt
}

public class Enemy : MonoBehaviour
{
    public string enemyName = "Enemy";
    public EnemyState currentState;

    private Transform _target;

    private Animator _animator;

    private static readonly int AttackAnimation = Animator.StringToHash("attack");
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Attack()
    {
        StartCoroutine(AttackCo());
    }

    private IEnumerator AttackCo()
    {
        _animator.SetTrigger(AttackAnimation);
        currentState = EnemyState.Attack;
        yield return new WaitForSeconds(1f);
        currentState = EnemyState.Idle;
    }

    public void Knock(Rigidbody2D myRigidBody, float knockTime)
    {
        StartCoroutine(KnockCo(myRigidBody, knockTime));
    }

    private IEnumerator KnockCo(Rigidbody2D myRigidBody, float knockTime)
    {
        if (myRigidBody != null && currentState == EnemyState.Hurt)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidBody.velocity = Vector2.zero;
            currentState = EnemyState.Idle;
        }
    }
}
