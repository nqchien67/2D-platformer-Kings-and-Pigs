using BehaviourTree;
using UnityEngine;

namespace Enemy
{
    public class MoveOutOfExplosionRange : EnemyNode
    {
        public MoveOutOfExplosionRange(Pig enemy) : base(enemy) { }

        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, 1f, data.bombLayer);
            if (collider != null)
            {
                Transform bomb = collider.transform;
                if (bomb.GetComponent<Bomb>().TimeCounter < 0.3f)
                {
                    float direction = bomb.position.x - transform.position.x > 0 ? -1 : 1;
                    core.SetVelocityX(direction * data.sprintSpeed);
                    return State.RUNNING;
                }
            }
            return State.SUCCESS;
        }
    }
}
