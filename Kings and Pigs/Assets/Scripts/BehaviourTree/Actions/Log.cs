using UnityEngine;

namespace BehaviourTree
{
    public class Log : ActionNode
    {
        public string message;

        public Log(string message)
        {
            this.message = message;
        }

        protected override void OnStart()
        {
            Debug.Log($"OnStart{message}");
        }

        protected override void OnStop()
        {
            //Debug.Log($"OnStop{message}");
        }

        protected override State OnUpdate()
        {
            //Debug.Log($"OnUpdate{message}");
            return State.SUCCESS;
        }
    }
}
