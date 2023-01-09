using BehaviourTree;
using UnityEngine;

namespace Enemy
{
    public class CheckBombNearby : EnemyNode
    {
        public CheckBombNearby(Pig enemy) : base(enemy) { }

        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            bool randomBool = Random.value < 0.3f;
            if (randomBool || blackboard.hasProjectile)
                return State.FAILURE;

            Vector2 origin;
            origin.x = transform.position.x - data.pickRange / 2;
            origin.y = transform.position.y - 0.2f;

            RaycastHit2D bomb = Physics2D.Raycast(origin, Vector2.right, data.pickRange, data.bombLayer);
            if (bomb && bomb.rigidbody.velocity == Vector2.zero)
            {
                blackboard.projectileObj = bomb.collider.gameObject;
                return State.SUCCESS;
            }

            return State.FAILURE;
        }
    }
}
