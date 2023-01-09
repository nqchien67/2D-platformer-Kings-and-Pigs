namespace BehaviourTree
{
    public abstract class DecoratorNode : Node
    {
        public Node child;

        public DecoratorNode(Node child)
        {
            this.child = child;
        }
    }
}
