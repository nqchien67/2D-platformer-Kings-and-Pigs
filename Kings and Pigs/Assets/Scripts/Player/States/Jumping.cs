using UnityEngine;

namespace Player
{
    public class Jumping : Movement
    {
        private float jumpForce;

        public Jumping(King stateMachine) : base("Jumping", stateMachine) { }

        public override void Enter()
        {
            base.Enter();
            jumpForce = player.data.jumpForce;
            core.SetVelocity(rigidbody.velocity.x, jumpForce);
        }

        public override void Update()
        {
            base.Update();
            stateMachine.ChangeState(player.inAirState);
        }
    }
}
