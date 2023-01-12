﻿using FiniteStateMachine;
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

            if (Input.GetKeyDown(KeyCode.S) && !core.isKnockbacking)
                stateMachine.ChangeState(player.attackStage);
            else if (!isGrounded)
                stateMachine.ChangeState(player.inAirState);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            isGrounded = core.IsTouchingGround();
        }
    }
}
