using UnityEngine;

namespace Player
{
    public class Grounding : Movement
    {
        private float lastGroundTime;
        private float LastPressedJumpTime;

        public Grounding(string name, King stateMachine) : base(name, stateMachine)
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

            lastGroundTime -= Time.deltaTime;
            LastPressedJumpTime -= Time.deltaTime;

            if (core.IsTouchingGround())
                lastGroundTime = data.coyoteTime;

            if (Input.GetKeyDown(KeyCode.A))
                LastPressedJumpTime = data.jumpInputBufferTime;

            if (LastPressedJumpTime > 0 && lastGroundTime > 0)
            {
                lastGroundTime = 0;
                LastPressedJumpTime = 0;
                player.ChangeState(player.jumpingState);
            }
        }
    }
}
