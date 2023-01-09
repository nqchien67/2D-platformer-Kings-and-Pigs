using BehaviourTree;
using UnityEngine;

namespace Enemy
{
    public class MeleeAttack : EnemyNode
    {
        private float attackCounter = 0;

        public MeleeAttack(Pig enemy) : base(enemy) { }

        protected override void OnStart()
        {
            core.SetVelocityX(0f);
            animator.SetBool("Moving", false);
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (Time.time - attackCounter > data.meleeTime)
            {
                attackCounter = Time.time;
                animator.SetTrigger("Attack");
            }

            return State.SUCCESS;
        }

        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Gizmos.DrawWireSphere(enemy.AttackTransform.position, data.meleeRadius);
            Gizmos.DrawRay(enemy.AttackTransform.position, -transform.right * data.meleeRadius);
        }
    }
}