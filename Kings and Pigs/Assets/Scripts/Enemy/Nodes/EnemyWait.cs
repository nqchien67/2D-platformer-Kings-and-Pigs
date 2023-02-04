using BehaviourTree;

namespace Enemy
{
    public class EnemyWait : Wait
    {
        private Pig pig;

        public EnemyWait(Pig pig) : base()
        {
            this.pig = pig;
        }

        public EnemyWait(Pig pig, float duration) : base(duration)
        {
            this.pig = pig;
        }

        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnStop()
        {
            base.OnStop();
        }

        protected override State OnUpdate()
        {
            pig.Core.SetVelocityX(0);
            return base.OnUpdate();
        }
    }
}