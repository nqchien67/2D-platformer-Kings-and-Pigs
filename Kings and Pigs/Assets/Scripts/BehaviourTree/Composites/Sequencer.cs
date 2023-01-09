using System.Collections.Generic;

namespace BehaviourTree
{
    public class Sequencer : CompositeNode
    {
        private int current;

        public Sequencer(List<Node> children) : base(children)
        {
        }

        protected override void OnStart()
        {
            current = 0;
        }

        protected override void OnStop() { }

        protected override State OnUpdate()
        {
            for (int i = current; i < children.Count; ++i)
            {
                current = i;
                Node child = children[current];

                switch (child.Update())
                {
                    case State.RUNNING:
                        return State.RUNNING;
                    case State.FAILURE:
                        return State.FAILURE;
                    case State.SUCCESS:
                        continue;
                }
            }

            return State.SUCCESS;
        }
    }
}