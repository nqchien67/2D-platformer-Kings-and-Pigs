using FiniteStateMachine;
using UnityEngine;

namespace Player
{
    public class Attack : BaseState
    {
        private King player;
        private Core core;
        private PlayerData data;

        public bool isAttackDone;
        private bool isGrounded;
        private bool ableToFlip;

        public Attack(King player) : base("Attack", player)
        {
            this.player = player;
            core = player.core;
            data = player.data;
        }

        public override void Enter()
        {
            base.Enter();
            player.Animator.SetTrigger("Attack");
            isAttackDone = false;
        }

        public override void Update()
        {
            base.Update();

            if (ableToFlip)
                player.CheckIfShouldFlip(player.xInput);

            core.SetVelocityX(player.xInput * player.data.movingSpeed);

            if (isAttackDone || core.isKnockbacking)
            {
                if (isGrounded && player.Rigidbody.velocity.y < 0.01f)
                    stateMachine.ChangeState(player.idleStage);
                else
                    stateMachine.ChangeState(player.jumpingState);
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            isGrounded = core.IsTouchingGround();
        }

        public override void Exit()
        {
            base.Exit();
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

        //public void AddToDetectdList(Collider2D collision)
        //{
        //    IDamageable damageable = collision.GetComponent<IDamageable>();
        //    if (damageable != null)
        //        detectedDamageables.Add(damageable);

        //    IKnockbackable knockbackable = collision.GetComponent<IKnockbackable>();
        //    if (knockbackable != null)
        //        detectedKnockbackables.Add(knockbackable);
        //}

        //public void RemoveFromDetectdList(Collider2D collision)
        //{
        //    IDamageable damageable = collision.GetComponent<IDamageable>();
        //    if (damageable != null)
        //        detectedDamageables.Remove(damageable);

        //    IKnockbackable knockbackable = collision.GetComponent<IKnockbackable>();
        //    if (knockbackable != null)
        //        detectedKnockbackables.Remove(knockbackable);
        //}
    }
}
