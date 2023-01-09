using System.Collections.Generic;

namespace BehaviourTree
{
    public abstract class CompositeNode : Node
    {
        public List<Node> children = new List<Node>();

        public CompositeNode(List<Node> children)
        {
            this.children = children;
        }
    }
}
