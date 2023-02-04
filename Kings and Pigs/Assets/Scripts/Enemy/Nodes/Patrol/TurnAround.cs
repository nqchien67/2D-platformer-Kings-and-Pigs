using BehaviourTree;

namespace Enemy
{
    public class TurnAround : EnemyNode
    {
        public TurnAround(Pig enemy) : base(enemy) { }

        protected override void OnStart()
        {

        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (core.isKnockbacking)
                return State.RUNNING;

            core.Flip();
            return State.SUCCESS;
        }
    }
}
