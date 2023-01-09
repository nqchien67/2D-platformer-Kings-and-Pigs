namespace BehaviourTree
{
    public enum State
    {
        RUNNING,
        SUCCESS,
        FAILURE,
    }

    public abstract class Node
    {
        public State state = State.RUNNING;
        public bool started = false;

        public State Update()
        {
            if (!started)
            {
                OnStart();
                started = true;
            }

            state = OnUpdate();

            if (state == State.FAILURE || state == State.SUCCESS)
            {
                OnStop();
                started = false;
            }

            return state;
        }

        public void Abort()
        {
            BehaviourTree.Traverse(this, (node) =>
            {
                node.started = false;
                node.state = State.RUNNING;
                node.OnStop();
            });
        }

        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract State OnUpdate();

        public virtual void OnDrawGizmos() { }
    }
}
