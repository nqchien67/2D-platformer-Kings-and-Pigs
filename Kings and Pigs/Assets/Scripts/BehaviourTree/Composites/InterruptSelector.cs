using System.Collections.Generic;

namespace BehaviourTree
{
    public class InterruptSelector : Selector
    {
        public InterruptSelector(List<Node> children) : base(children) { }

        protected override State OnUpdate()
        {
            int previous = current;
            base.OnStart();
            var status = base.OnUpdate();
            if (previous != current)
            {
                if (children[previous].state == State.RUNNING)
                {
                    children[previous].Abort();
                }
            }

            return status;
        }
    }
}
