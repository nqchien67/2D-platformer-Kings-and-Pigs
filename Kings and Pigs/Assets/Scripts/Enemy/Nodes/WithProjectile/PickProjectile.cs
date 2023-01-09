using BehaviourTree;
using UnityEngine;

namespace Enemy
{
    public class PickProjectile : EnemyNode
    {
        public PickProjectile(Pig enemy) : base(enemy) { }

        protected override void OnStart()
        {
            core.SetVelocityX(0);
            blackboard.hasProjectile = true;

            string projectileType = blackboard.projectileObj.tag;
            blackboard.projectileType = projectileType;

            MoveProjectileToInventory();

            switch (projectileType)
            {
                case "Bomb":
                    animator.SetTrigger("PickBomb");
                    animator.SetBool("HasBomb", true);
                    break;

                case "Box":
                    animator.SetTrigger("PickBox");
                    animator.SetBool("HasBox", true);
                    break;
            }
            animator.SetBool("Moving", false);
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            return State.SUCCESS;
        }

        private void MoveProjectileToInventory()
        {
            //blackboard.projectileObj.transform.position = enemy.PigInventoryPosition;
            blackboard.projectileObj.SetActive(false);
        }
    }
}
