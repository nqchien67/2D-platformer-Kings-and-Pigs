using UnityEngine;

namespace BehaviourTree
{
    public abstract class Context
    {
        protected GameObject gameObject;

        public Context() { }

        public Context(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }
}
