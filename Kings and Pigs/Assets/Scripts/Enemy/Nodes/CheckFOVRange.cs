using BehaviourTree;
using UnityEngine;

namespace Enemy
{
    public class CheckFOVRange : EnemyNode
    {
        private bool trackingTarget = false;
        private Vector2 pointA;
        private Vector2 pointB;

        public CheckFOVRange(Pig enemy) : base(enemy)
        {
        }

        protected override void OnStart()
        {
            pointA.Set(enemy.ActiveArea[0].x - data.fovRange, enemy.ActiveArea[0].y + 0.3f * data.fovRange);
            pointB.Set(enemy.ActiveArea[1].x + data.fovRange, enemy.ActiveArea[1].y - 0.3f * data.fovRange);
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            RaycastHit2D target = CheckFOV();
            if (target)
            {
                trackingTarget = true;
                blackboard.target = target.transform;

                dialogue.PlayerEncounter();
                return State.SUCCESS;
            }

            Collider2D collier = Physics2D.OverlapArea(pointA, pointB, data.playerLayer);
            if (trackingTarget && collier)
            {
                blackboard.target = collier.transform;
                return State.SUCCESS;
            }
            else
            {
                trackingTarget = false;
                blackboard.target = null;
                dialogue.ResetFirstEncounter();
            }

            return State.FAILURE;
        }

        private RaycastHit2D CheckFOV()
        {
            RaycastHit2D raycastHit1 = Physics2D.Raycast(transform.position, -transform.right, data.fovRange, data.playerLayer);
            if (raycastHit1)
                return raycastHit1;

            else
            {
                Vector2 r2Origin = transform.position;
                r2Origin.y += data.fovWidth;
                RaycastHit2D raycastHit2 = Physics2D.Raycast(r2Origin, -transform.right, data.fovRange, data.playerLayer);

                if (raycastHit2)
                    return raycastHit2;
            }

            Vector2 r3Origin = transform.position;
            r3Origin.y -= data.fovWidth;
            RaycastHit2D raycastHit3 = Physics2D.Raycast(r3Origin, -transform.right, data.fovRange, data.playerLayer);
            return raycastHit3;
        }

        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            //Gizmos.DrawWireSphere(trackingMidpoint, trackingRadius);
            //Gizmos.DrawRay(transform.position, -transform.right * data.fovRange);
            //Vector2 direction = -transform.right;
            //direction.x *= data.fovRange;
            //Gizmos.DrawRay(transform.position, direction);
            //Gizmos.DrawRay(new Vector2(transform.position.x, transform.position.y + data.fovWidth), direction);
            //Gizmos.DrawRay(new Vector2(transform.position.x, transform.position.y - data.fovWidth), direction);
        }
    }
}
