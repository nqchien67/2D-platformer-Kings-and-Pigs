using UnityEngine;

namespace Player
{
    public class Jumping : Movement
    {
        private bool isGrounded;
        private bool isHoldingJumpButton;
        private int jumpCounter;
        private float jumpForce;

        public Jumping(Player stateMachine) : base("Jumping", stateMachine) { }

        public override void Enter()
        {
            base.Enter();
            jumpForce = player.data.jumpForce;
            //jumpCounter = 0;

            core.SetVelocity(rigidbody.velocity.x, jumpForce);

            isGrounded = false;
            player.Animator.SetBool("Grounded", false);
        }

        public override void Update()
        {
            base.Update();
            //isHoldingJumpButton = Input.GetKey(KeyCode.A);

            player.CheckIfShouldFlip(xInput);

            //float velocityY = rigidbody.velocity.y;
            //if (jumpCounter < player.data.jumpStep &&
            //    jumpForce > Mathf.Epsilon &&
            //    isHoldingJumpButton)
            //{
            //    velocityY = jumpForce;
            //    jumpCounter++;
            //    jumpForce -= 0.05f;
            //}
            //core.SetVelocity(xInput * player.data.movingSpeed, velocityY);

            core.SetVelocityX(xInput * player.data.movingSpeed);

            player.Animator.SetFloat("YVelocity", rigidbody.velocity.y);

            if (isGrounded && rigidbody.velocity.y < 0.01f)
                stateMachine.ChangeState(player.idleStage);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            isGrounded = core.IsTouchingGround();
        }
    }
}
