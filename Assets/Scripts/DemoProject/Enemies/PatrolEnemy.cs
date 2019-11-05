using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies
{
    public abstract class PatrolEnemy : DemoEnemy
    {
        [FormerlySerializedAs("MaxSpeed")] public float maxSpeed = 4f;
        [FormerlySerializedAs("PatrolRange")] public float patrolRange = 0.7f;
        [FormerlySerializedAs("TopTrigger")] public Transform topTrigger;
        [FormerlySerializedAs("BottomTrigger")] public Transform bottomTrigger;
        
        protected bool IsMoving = true;

        
        public void StopMove()
        {
            IsMoving = false;
        }

        public void StartMove()
        {
            IsMoving = true;
        }
        
        protected override void ComputeVelocity()
        {
            base.ComputeVelocity();
                        
            if (CheckTopTrigger() || CheckBottomTrigger() || CheckWallTrigger())
            {
                ChangeDirection();
            }

            if (!IsDead && IsMoving) TargetVelocity = Direction * maxSpeed;
        }

        protected bool CheckTopTrigger()
        {
            Debug.DrawRay(topTrigger.position, Direction * patrolRange);
            
            RaycastHit2D topHit = Physics2D.Raycast(topTrigger.position, Direction, patrolRange);

            if (topHit == true)
                if (topHit.collider.CompareTag("Ground"))
                    return true;

            return false;
        }

        protected bool CheckBottomTrigger()
        {
            Debug.DrawRay(bottomTrigger.position, Direction * patrolRange);

            RaycastHit2D bottomHit = Physics2D.Raycast(bottomTrigger.position, Direction, patrolRange);

            if (bottomHit == true)
                if (bottomHit.collider.CompareTag("Ground"))
                    return true;

            return false;
        }
    }
}