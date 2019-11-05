using UnityEngine;

namespace Enemies
{
    public class Skeleton : PatrolEnemy
    {
        protected override void Start()
        {
            base.Start();
            
            StopMove();
            Invoke("StartMove", 1f);
        }
    }
}
