using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BehaviourTree
{
    [System.Serializable]
    public class RandomSelector : CompositeNode
    {
        protected int current;

        public RandomSelector(List<Node> children) : base(children) { }

        protected override void OnStart()
        {
            current = Random.Range(0, children.Count);
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            var child = children[current];
            return child.Update();
        }
    }
}