using BehaviourTree;

namespace Enemy
{
    public class Stare : EnemyNode
    {
        public Stare(Pig enemy) : base(enemy) { }

        protected override void OnStart()
        {
            core.SetVelocityX(0f);
            enemy.FacingToPosition(blackboard.target.position);
            animator.SetBool("Moving", false);
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            return State.SUCCESS;
        }
    }
}
