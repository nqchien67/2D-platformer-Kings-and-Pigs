namespace BehaviourTree
{
    public class Succeed : DecoratorNode
    {
        public Succeed(Node child) : base(child) { }

        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            var state = child.Update();
            if (state == State.FAILURE)
            {
                return State.SUCCESS;
            }
            return state;
        }
    }
}