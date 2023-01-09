using BehaviourTree;
using UnityEngine;

namespace Enemy
{
    public class Charge : EnemyNode
    {
        public Charge(Pig enemy) : base(enemy) { }

        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (TargetInMeleeRange()) return State.SUCCESS;

            Vector2 playerPosition = GameObject.Find("King").transform.position;
            if (CantReachTarget(playerPosition))
                return State.FAILURE;

            enemy.MoveToPosition(playerPosition, data.sprintSpeed);
            animator.SetBool("Moving", true);

            return State.RUNNING;
        }

        private bool TargetInMeleeRange()
        {
            return Physics2D.Raycast(enemy.AttackTransform.position, -transform.right, data.meleeRadius, data.playerLayer);
        }

        private bool CantReachTarget(Vector2 playerPosition)
        {
            bool isTargetTooHigh = Mathf.Abs(playerPosition.y - transform.position.y) > 0.5f;
            return enemy.IsDetectedLedge() || isTargetTooHigh;
        }
    }
}