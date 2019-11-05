using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies
{
    public abstract class DemoEnemy : LiveThing
    {
        [FormerlySerializedAs("PlayerDamage")]
        public float playerDamage; //The amount of health points to subtract from the player when attacking.

        [FormerlySerializedAs("MaxDistanceOfPlayerDetection")]
        public float maxDistanceOfPlayerDetection = 15f;

        [FormerlySerializedAs("MaxAngleOFPlayerDetection")]
        public float maxAngleOfPlayerDetection = 60f;

        protected DemoPlayer Player;
        protected bool IsSeePlayer = false;

        protected override void Start()
        {
            base.Start();

            Player = GameMaster.GetPlayer();
        }

        protected override void ComputeVelocity()
        {
            base.ComputeVelocity();

            Track();
            DrawLines();
        }

        private void Track()
        {
            var directionToTarget = transform.position - Player.transform.position; // distance
            var angle = Vector3.Angle(Direction * -1, directionToTarget); // angle between player and tower
            var distance = directionToTarget.magnitude; // length of vector

            // if player is visible
            if (Mathf.Abs(angle) < maxAngleOfPlayerDetection && distance < maxDistanceOfPlayerDetection)
            {
                Debug.DrawRay(WallTrigger.position,
                    (Player.transform.position - transform.position).normalized *
                    maxDistanceOfPlayerDetection);

                RaycastHit2D playerHit = Physics2D.Raycast(WallTrigger.position,
                    (Player.transform.position - transform.position).normalized,
                    maxDistanceOfPlayerDetection);


                if (playerHit == true)
                    if (playerHit.collider.CompareTag("Player"))
                        IsSeePlayer = true;
            }
            else
            {
                IsSeePlayer = false;
            }
        }

        private void DrawLines()
        {
            var line = WallTrigger.position + Direction * maxDistanceOfPlayerDetection;
            var rotatedLine = Quaternion.AngleAxis(0, transform.up) * line;
            Debug.DrawLine(WallTrigger.position, rotatedLine, Color.red);


//        var lineTop = transform.position + Direction * -1 * maxDistance; // 25 - length offset, don't work correctly
//        var rotatedLineTop = Quaternion.AngleAxis(maxAngle * drawOffset * -1, transform.forward) * lineTop;
//        Debug.DrawLine(transform.position, rotatedLineTop, Color.blue);
//
//        var lineBottom = transform.position + Direction * -1 * maxDistance;
//        var rotatedLineBottom = Quaternion.AngleAxis(maxAngle * drawOffset, transform.forward) * lineBottom;
//        Debug.DrawLine(transform.position, rotatedLineBottom, Color.blue);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!IsDead)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    other.gameObject.GetComponent<DemoPlayer>().ApplyDamage(playerDamage);
                }
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (!IsDead)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    other.gameObject.GetComponent<DemoPlayer>().ApplyDamage(playerDamage);
                }
            }
        }
    }
}