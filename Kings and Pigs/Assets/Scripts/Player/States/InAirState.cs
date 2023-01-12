using UnityEngine;

namespace Player
{
    public class InAirState : Movement
    {
        public InAirState(King player) : base("In air", player) { }

        public override void Enter()
        {
            base.Enter();
            animator.SetBool("Grounded", false);
        }

        public override void Update()
        {
            base.Update();

            player.CheckIfShouldFlip(xInput);
            core.SetVelocityX(xInput * player.data.movingSpeed);

            animator.SetFloat("YVelocity", rigidbody.velocity.y);

            if (isGrounded && rigidbody.velocity.y < 0.01f)
                stateMachine.ChangeState(player.idleStage);
        }
    }
}