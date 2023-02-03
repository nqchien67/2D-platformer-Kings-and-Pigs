using FiniteStateMachine;
using UnityEngine;

namespace Player
{
    public class Movement : BaseState
    {
        protected King player;
        protected Core core;
        protected PlayerData data;
        protected Rigidbody2D rigidbody;
        protected Animator animator;

        protected float xInput;
        protected bool isGrounded;

        public Movement(string name, King player) : base(name, player)
        {
            this.player = player;
            core = player.core;
            data = player.data;
            rigidbody = player.Rigidbody;
            animator = player.Animator;
        }

        public override void Update()
        {
            base.Update();

            xInput = player.xInput;
            isGrounded = core.IsTouchingGround();

            if (Input.GetKeyDown(KeyCode.D) && !core.isKnockbacking)
                stateMachine.ChangeState(player.attackStage);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }
    }
}
