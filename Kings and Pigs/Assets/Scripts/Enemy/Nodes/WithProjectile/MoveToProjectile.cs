using BehaviourTree;
using UnityEngine;

namespace Enemy
{
    public class MoveToProjectile : EnemyNode
    {
        private Vector2 projectilePosition;

        public MoveToProjectile(Pig enemy) : base(enemy) { }

        protected override void OnStart()
        {
            projectilePosition = blackboard.projectileObj.transform.position;

            float moveSpeed = blackboard.target ? data.sprintSpeed : data.walkSpeed;
            enemy.MoveToPosition(projectilePosition, moveSpeed);
            animator.SetBool("Moving", true);
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (Mathf.Abs(transform.position.x - projectilePosition.x) < 0.5f)
                return State.SUCCESS;

            if (enemy.IsDetectedLedge()) return State.FAILURE;

            return State.RUNNING;
        }
    }
}
