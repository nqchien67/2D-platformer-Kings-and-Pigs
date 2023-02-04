using BehaviourTree;
using UnityEngine;

namespace Enemy
{
    public class MoveToProjectile : EnemyNode
    {
        private Vector2 projectilePosition;
        private float moveSpeed;

        public MoveToProjectile(Pig enemy) : base(enemy) { }

        protected override void OnStart()
        {
            projectilePosition = blackboard.projectileObj.transform.position;

            moveSpeed = blackboard.target ? data.sprintSpeed : data.walkSpeed;
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

            enemy.MoveToPosition(projectilePosition, moveSpeed);
            return State.RUNNING;
        }
    }
}
