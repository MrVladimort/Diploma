using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Attack,
    Hurt,
    Patrolling,
    Chasing
}

public class Enemy : MoveableObject
{
    public string enemyName = "Enemy";
    public EnemyState currentState;
    public float maxAngleOfPlayerDetection = 15f;
    public float maxDistanceOfPlayerDetection = 60f;
    public Transform trackPoint;

    [HideInInspector] public Player player;
    [HideInInspector] public bool seePlayer;

    private GameMaster gameMaster;
    private Animator animator;

    private static readonly int AttackAnimation = Animator.StringToHash("attack");

    // Start is called before the first frame update
    protected void Start()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        animator = GetComponent<Animator>();
        player = gameMaster.GetPlayer();
    }

    // Update is called once per frame
    protected void Update()
    {
        Track();
    }

    private void Track()
    {
        DrawLines();

        var position = transform.position;
        var playerPosition = player.transform.position;

        var directionToTarget = position - playerPosition; // distance
        var angle = Vector3.Angle(direction * -1, directionToTarget); // angle between player and tower
        var distance = directionToTarget.magnitude; // length of vector

        // if player is visible
        if (Mathf.Abs(angle) < maxAngleOfPlayerDetection && distance < maxDistanceOfPlayerDetection)
        {
            var trackPosition = trackPoint.position;

            Debug.DrawRay(trackPosition,
                (playerPosition - position).normalized *
                maxDistanceOfPlayerDetection);

            RaycastHit2D playerHit = Physics2D.Raycast(trackPosition,
                (playerPosition - position).normalized,
                maxDistanceOfPlayerDetection);


            if (playerHit == true)
                if (playerHit.collider.CompareTag("Player"))
                    seePlayer = true;
        }
        else
        {
            seePlayer = false;
        }
    }

    private void DrawLines()
    {
        var forward = trackPoint.forward;
        var trackPosition = trackPoint.position;

        var line = trackPosition + direction * maxDistanceOfPlayerDetection;
        var rotatedLine = Quaternion.AngleAxis(0, transform.up) * line;
        Debug.DrawLine(trackPosition, rotatedLine, Color.red);

        var lineTop =
            trackPosition + maxDistanceOfPlayerDetection * direction; // 25 - length offset, don't work correctly
        var rotatedLineTop = Quaternion.AngleAxis(maxAngleOfPlayerDetection * -1, forward) * lineTop;
        Debug.DrawLine(trackPosition, rotatedLineTop, Color.blue);

        var lineBottom = trackPosition + maxDistanceOfPlayerDetection * direction;
        var rotatedLineBottom = Quaternion.AngleAxis(maxAngleOfPlayerDetection, forward) * lineBottom;
        Debug.DrawLine(trackPosition, rotatedLineBottom, Color.blue);
    }

    public void Attack()
    {
        StartCoroutine(AttackCo());
    }

    private IEnumerator AttackCo()
    {
        var previousState = currentState;
        animator.SetTrigger(AttackAnimation);
        currentState = EnemyState.Attack;
        yield return new WaitForSeconds(1f);
        currentState = previousState;
    }

    public void Knock(Rigidbody2D myRigidBody, float knockTime)
    {
        StartCoroutine(KnockCo(myRigidBody, knockTime));
    }

    private IEnumerator KnockCo(Rigidbody2D myRigidBody, float knockTime)
    {
        if (myRigidBody != null && currentState == EnemyState.Hurt)
        {
            var previousState = currentState;
            yield return new WaitForSeconds(knockTime);
            myRigidBody.velocity = Vector2.zero;
            currentState = previousState;
        }
    }
}