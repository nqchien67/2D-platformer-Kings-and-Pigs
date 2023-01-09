using BehaviourTree;
using UnityEngine;

namespace Enemy
{
    public abstract class EnemyNode : ActionNode
    {
        protected Pig enemy;
        protected Core core;
        protected Transform transform;
        protected Rigidbody2D rigidbody;
        protected Animator animator;
        protected Dialogue dialogue;
        protected EnemyData data;
        protected Blackboard blackboard;

        public EnemyNode(Pig enemy)
        {
            this.enemy = enemy;
            core = enemy.Core;
            blackboard = enemy.Blackboard;
            transform = enemy.transform;
            rigidbody = enemy.Rigidbody;
            animator = transform.GetComponent<Animator>();
            dialogue = transform.GetComponentInChildren<Dialogue>();
            data = enemy.data;
        }

        protected abstract override void OnStart();
        protected abstract override void OnStop();
        protected abstract override State OnUpdate();
    }
}
