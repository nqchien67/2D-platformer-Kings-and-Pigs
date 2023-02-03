using UnityEngine;

namespace Player
{
    public class InAirState : Movement
    {
        private float coyoteTime = 0f;
        private float lastPressedJumpTime = 0f;

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

            JumpBufferHandle();

            if (isGrounded && rigidbody.velocity.y < 0.01f)
                OnLanding();
            else if (IsJumpCut())
            {
                core.SetVelocityY(rigidbody.velocity.y * 0.3f);
            }
            else if (coyoteTime > 0)
            {
                coyoteTime -= Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.S))
                {
                    coyoteTime = 0f;
                    stateMachine.ChangeState(player.jumpingState);
                }
            }
        }

        private void JumpBufferHandle()
        {
            if (Input.GetKeyDown(KeyCode.S))
                lastPressedJumpTime = data.jumpInputBufferTime;

            if (lastPressedJumpTime > 0)
                lastPressedJumpTime -= Time.deltaTime;
        }

        private void OnLanding()
        {
            if (lastPressedJumpTime > 0)
            {
                lastPressedJumpTime = 0f;
                animator.SetBool("Grounded", true);
                stateMachine.ChangeState(player.jumpingState);
            }
            else
                stateMachine.ChangeState(player.idleStage);
        }

        private bool IsJumpCut() => Input.GetKeyUp(KeyCode.S) && rigidbody.velocity.y > 0f;

        public void StartCoyoteTime()
        {
            coyoteTime = data.coyoteTime;
        }
    }
}