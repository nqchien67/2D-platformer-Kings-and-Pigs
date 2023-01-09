using UnityEngine;

namespace BehaviourTree
{
    public class Wait : ActionNode
    {
        public float duration;
        private float startTime;

        public Wait(float duration = 0.5f)
        {
            this.duration = duration;
        }

        protected override void OnStart()
        {
            startTime = Time.time;
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            float timeRemaining = Time.time - startTime;
            if (timeRemaining > duration)
                return State.SUCCESS;

            return State.RUNNING;
        }
    }
}
