﻿using FiniteStateMachine;
using UnityEngine;

namespace Player
{
    public class Player : StateMachine, ICharacterController
    {
        public Rigidbody2D Rigidbody { get; private set; }
        public Animator Animator { get; private set; }
        public Transform AttackTransform { get; private set; }
        public Core core { get; private set; }

        [HideInInspector] public float xInput;
        public PlayerData data;

        public Idle idleStage;
        public Moving runningStage;
        public Jumping jumpingState;
        public Landing landingStage;
        public Attack attackStage;

        private void Awake()
        {
            core = GetComponentInChildren<Core>();
            AttackTransform = transform.GetChild(0);

            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();

            idleStage = new Idle(this);
            runningStage = new Moving(this);
            jumpingState = new Jumping(this);
            landingStage = new Landing(this);
            attackStage = new Attack(this);

            core.isFacingRight = true;
        }

        protected override void Update()
        {
            base.Update();
            xInput = Input.GetAxisRaw("Horizontal");
        }

        protected override BaseState GetInitialState()
        {
            return idleStage;
        }

        public void CheckIfShouldFlip(float horizontalInput)
        {
            bool isFacingRight = core.isFacingRight;
            if (isFacingRight && horizontalInput < 0)
                core.Flip();
            else if (!isFacingRight && horizontalInput > 0)
                core.Flip();
        }

        public void Die() { this.enabled = false; }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("ExitDoor"))
            {
                if (Input.GetKey(KeyCode.UpArrow) && core.IsTouchingGround())
                {
                    //other.GetComponent<SceneLoader>().LoadNextScene();
                }
            }
        }

        #region Animation triggers
        //public void AnimationActionTrigger() { attackStage.DoDamage(); }

        public void AnimationFinishTrigger() { attackStage.SetAttackDone(); }

        public void AnimationTurnOffFlipTrigger() { attackStage.TurnOffFlip(); }

        public void AnimationTurnOnFlipTrigger() { attackStage.TurnOnFlip(); }
        #endregion
    }
}
