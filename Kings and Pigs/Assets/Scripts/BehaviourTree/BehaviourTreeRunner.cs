using UnityEngine;

namespace BehaviourTree
{
    public abstract class BehaviourTreeRunner : MonoBehaviour
    {
        private BehaviourTree tree;

        private void Start()
        {
            tree = new BehaviourTree();
            tree.rootNode = SetupTree();
        }

        protected abstract Node SetupTree();

        protected virtual void Update()
        {
            tree.Update();
        }

        protected virtual void OnDrawGizmos()
        {
            if (tree == null) return;

            BehaviourTree.Traverse(tree.rootNode, (node) => node.OnDrawGizmos());
        }
    }
}
