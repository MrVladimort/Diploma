using UnityEngine;

namespace Enemies
{
    public abstract class ProjectileEnemy : PatrolEnemy
    {
        public Transform startBulletPosition;
        public GameObject bullet;

        public float period = 1f;
        private float lastBulletTime = 0;

        // Use this for initialization
        // Update is called once per frame

        protected override void ComputeVelocity()
        {
            base.ComputeVelocity();
            
            if (IsSeePlayer && !IsDead)
            {
                StopMove();
                Shoot();
            }
            else
            {
                StartMove();
            }
        }

        private void Shoot()
        {
            lastBulletTime += Time.deltaTime;

            if (lastBulletTime > period)
            {
                Animator.SetTrigger("attack");
                lastBulletTime = 0;
                var newBullet = Instantiate(bullet, startBulletPosition.transform.position, transform.rotation);
                newBullet.GetComponent<Projectile>().SetTarget(Player.transform);
            }
        }
    }
}