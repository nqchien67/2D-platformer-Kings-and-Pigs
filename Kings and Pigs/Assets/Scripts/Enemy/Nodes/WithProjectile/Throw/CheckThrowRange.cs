using BehaviourTree;
using UnityEngine;

namespace Enemy
{
    public class CheckThrowRange : EnemyNode
    {
        public CheckThrowRange(Pig enemy) : base(enemy) { }

        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (!blackboard.hasProjectile)
                return State.FAILURE;

            Collider2D playerInRange = Physics2D.OverlapCircle(transform.position, data.throwRange, data.playerLayer);
            if (playerInRange)
            {
                enemy.FacingToPosition(playerInRange.transform.position);
                return State.SUCCESS;
            }

            return State.FAILURE;
        }
    }
}
