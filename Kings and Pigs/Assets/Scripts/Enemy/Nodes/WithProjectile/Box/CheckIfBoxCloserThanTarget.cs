using BehaviourTree;
using UnityEngine;

namespace Enemy
{
    public class CheckIfBoxCloserThanTarget : EnemyNode
    {
        public CheckIfBoxCloserThanTarget(Pig enemy) : base(enemy) { }

        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            bool randomBool = Random.value < 0.1f;
            if (randomBool || blackboard.hasProjectile)
                return State.FAILURE;

            Vector2 origin;
            origin.x = transform.position.x - data.pickRange / 2;
            origin.y = transform.position.y - 0.2f;

            RaycastHit2D box = Physics2D.Raycast(origin, Vector2.right, data.pickRange, data.boxLayer);

            if (!box)
                return State.FAILURE;

            bool isBoxStill = box.rigidbody.velocity == Vector2.zero;
            if (box.transform.CompareTag("Box") && isBoxStill)
            {
                float targetDistance = Vector2.Distance(transform.position, blackboard.target.position);

                bool isBoxCloser = box.distance < targetDistance;
                bool isTargetOutsideCloseRange = targetDistance > data.closeRange;
                if (isTargetOutsideCloseRange || isBoxCloser)
                {
                    blackboard.projectileObj = box.collider.gameObject;
                    return State.SUCCESS;
                }
            }

            return State.FAILURE;
        }
    }
}