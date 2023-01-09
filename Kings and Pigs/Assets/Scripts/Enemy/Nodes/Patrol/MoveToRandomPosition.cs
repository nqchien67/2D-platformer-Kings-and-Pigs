using BehaviourTree;
using UnityEngine;

namespace Enemy
{
    public class MoveToRandomPosition : EnemyNode
    {
        private Vector2 pointToMove;

        public MoveToRandomPosition(Pig enemy) : base(enemy)
        {
            pointToMove.y = transform.position.y;
        }

        protected override void OnStart()
        {
            pointToMove.x = Random.Range(enemy.ActiveArea[0].x, enemy.ActiveArea[1].x);
            animator.SetBool("Moving", true);
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            enemy.MoveToPosition(pointToMove, data.walkSpeed);
            bool isRechedPoint = Mathf.Abs(transform.position.x - pointToMove.x) < 0.1f;
            if (isRechedPoint || enemy.IsDetectedLedge())
                return State.SUCCESS;

            if (core.isKnockbacking)
                return State.FAILURE;

            return State.RUNNING;
        }
    }
}
