using BehaviourTree;
using UnityEngine;

namespace Enemy
{
    public class ThrowProjectile : EnemyNode
    {
        private IProjectile projectileScript;

        public ThrowProjectile(Pig enemy) : base(enemy) { }

        protected override void OnStart()
        {
            blackboard.hasProjectile = false;
            core.SetVelocityX(0f);
            switch (blackboard.projectileType)
            {
                case "Bomb":
                    animator.SetBool("HasBomb", false);
                    break;
                case "Box":
                    animator.SetBool("HasBox", false);
                    break;
            }
            animator.SetBool("Moving", false);
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
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
    }
}