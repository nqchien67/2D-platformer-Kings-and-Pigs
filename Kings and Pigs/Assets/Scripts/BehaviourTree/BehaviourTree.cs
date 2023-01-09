using System.Collections.Generic;

namespace BehaviourTree
{
    public class BehaviourTree
    {
        public Node rootNode;
        protected State treeState = State.RUNNING;

        public State Update()
        {
            if (rootNode.state == State.RUNNING)
                treeState = rootNode.Update();

            return treeState;
        }

        public static void Traverse(Node node, System.Action<Node> visiter)
        {
            if (node != null)
            {
                visiter.Invoke(node);
                var children = GetChildren(node);
                children.ForEach((n) => Traverse(n, visiter));
            }
        }

        protected static List<Node> GetChildren(Node parent)
        {
            List<Node> children = new List<Node>();

            if (parent is DecoratorNode decorator && decorator.child != null)
            {
                children.Add(decorator.child);
            }

            if (parent is CompositeNode composite)
            {
                return composite.children;
            }

            return children;
        }
    }

}
