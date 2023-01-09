using UnityEngine;

namespace Player
{
    public class Moving : Grounding
    {
        public Moving(Player stateMachine) : base("Moving", stateMachine) { }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();

            player.CheckIfShouldFlip(xInput);

            if (Mathf.Abs(xInput) < Mathf.Epsilon)
                stateMachine.ChangeState(player.idleStage);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            core.SetVelocityX(xInput * player.data.movingSpeed);
        }

    }
}