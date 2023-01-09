using BehaviourTree;
using UnityEngine;

namespace Enemy
{
    public class Idle : EnemyNode
    {
        private float idleTime;
        private float startTime;

        public Idle(Pig enemy) : base(enemy) { }

        protected override void OnStart()
        {
            startTime = Time.time;
            idleTime = Random.Range(data.minWaitTime, data.maxWaitTime);
            animator.SetBool("Moving", false);
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            core.SetVelocityX(0);
            float timeRemaining = Time.time - startTime;
            if (timeRemaining > idleTime)
                return State.SUCCESS;

            if (core.isKnockbacking)
                return State.FAILURE;

            return State.RUNNING;
        }
    }
}
