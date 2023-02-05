using FiniteStateMachine;
using UnityEngine;

namespace Player
{
    public class Attack : BaseState
    {
        private King player;
        private Core core;
        private PlayerData data;
        private Animator animator;

        public bool isAttackDone;
        private bool isGrounded;
        private bool ableToFlip;
        private float lastPressedJumpTime = 0f;

        public Attack(King player) : base("Attack", player)
        {
            this.player = player;
            core = player.core;
            data = player.data;
            animator = player.Animator;
        }

        public override void Enter()
        {
            base.Enter();
            animator.SetTrigger("Attack");
            isAttackDone = false;
        }

        public override void Update()
        {
            base.Update();

            JumpBufferHandle();

            if (ableToFlip)
            {
                player.CheckIfShouldFlip(player.xInput);
                if (lastPressedJumpTime > 0)
                {
                    lastPressedJumpTime = 0f;
                    stateMachine.ChangeState(player.jumpingState);
                }
            }

            core.SetVelocityX(player.xInput * player.data.movingSpeed);

            if (isAttackDone || core.isKnockbacking)
            {
                if (isGrounded && player.Rigidbody.velocity.y < 0.01f)
                {
                    stateMachine.ChangeState(player.idleState);
                }
                else
                    stateMachine.ChangeState(player.inAirState);
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            isGrounded = core.IsTouchingGround();
        }

        private void JumpBufferHandle()
        {
            if (Input.GetKeyDown(KeyCode.S))
                lastPressedJumpTime = data.jumpInputBufferTime;

            if (lastPressedJumpTime > 0)
                lastPressedJumpTime -= Time.deltaTime;
        }

        #region Animation triggers
        public void TurnOffFlip()
        {
            ableToFlip = false;
        }

        public void TurnOnFlip()
        {
            ableToFlip = true;
        }

        public void SetAttackDone()
        {
            isAttackDone = true;
        }

        public void DoDamage(Collider2D other)
        {
            if (other.TryGetComponent<IDamageable>(out var damageable))
                damageable.TakeDamage(data.attackDamage);

            if (other.TryGetComponent<IKnockbackable>(out var knockbackable))
            {
                Vector2 kbAngle = other.transform.position - player.AttackTransform.position;
                knockbackable.Knockback(data.knockbackStrength, kbAngle);
            }
        }
        #endregion
    }
}
