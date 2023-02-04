using BehaviourTree;
using UnityEngine;

namespace Enemy
{
    public class ThrowProjectile : EnemyNode
    {
        private IProjectile projectileScript;
        private float startTime;
        private float throwAnimationLength = 0.4f;

        public ThrowProjectile(Pig enemy) : base(enemy) { }

        protected override void OnStart()
        {
            startTime = Time.time;

            animator.SetTrigger("ThrowBomb");
            animator.SetBool("Moving", false);
        }

        protected override void OnStop()
        {
            blackboard.hasProjectile = false;
            animator.SetBool("Has" + blackboard.projectileType, false);
        }

        protected override State OnUpdate()
        {
            float timeRemaining = Time.time - startTime;
            if (timeRemaining > throwAnimationLength)
            {
                GameObject projectile = blackboard.projectileObj;
                if (projectile == null)
                    return State.FAILURE;

                projectile.SetActive(true);
                projectile.transform.position = transform.position;
                projectileScript = projectile.GetComponent<IProjectile>();
                projectileScript.Throw(blackboard.target.position, transform);

                return State.SUCCESS;
            }

            core.SetVelocityX(0f);
            return State.RUNNING;
        }
    }
}