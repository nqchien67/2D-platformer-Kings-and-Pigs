using UnityEngine;

namespace Player
{
    public class Grounded : Movement
    {
        public Grounded(string name, King stateMachine) : base(name, stateMachine)
        {
            player = stateMachine;
        }

        public override void Enter()
        {
            base.Enter();
            animator.SetBool("Grounded", true);
        }

        public override void Update()
        {
            base.Update();

            if (!isGrounded)
            {
                player.inAirState.StartCoyoteTime();
                player.ChangeState(player.inAirState);
            }
            else if (Input.GetKeyDown(KeyCode.S))
                player.ChangeState(player.jumpingState);
        }
    }
}
