using BehaviourTree;
using UnityEngine;

namespace Enemy
{
    public class CheckMeleeRange : EnemyNode
    {
        public CheckMeleeRange(Pig enemy) : base(enemy) { }

        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (Physics2D.Raycast(enemy.AttackTransform.position, -transform.right, data.meleeRadius, data.playerLayer))
                return State.SUCCESS;

            return State.FAILURE;
        }
    }
}
