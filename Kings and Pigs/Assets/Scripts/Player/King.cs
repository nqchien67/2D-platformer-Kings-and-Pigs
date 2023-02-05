using FiniteStateMachine;
using UnityEngine;

namespace Player
{
    public class King : StateMachine, ICharacterController
    {
        public Rigidbody2D Rigidbody { get; private set; }
        public Animator Animator { get; private set; }
        public Transform AttackTransform { get; private set; }
        public Core core { get; private set; }

        [HideInInspector] public float xInput;
        public PlayerData data;

        public Idle idleState;
        public Moving movingState;
        public Jumping jumpingState;
        public Attack attackState;
        public InAirState inAirState;

        private void Awake()
        {
            core = GetComponentInChildren<Core>();
            AttackTransform = transform.GetChild(0);

            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();

            idleState = new Idle(this);
            movingState = new Moving(this);
            jumpingState = new Jumping(this);
            attackState = new Attack(this);
            inAirState = new InAirState(this);

            core.isFacingRight = true;
        }

        protected override void Update()
        {
            base.Update();
            xInput = Input.GetAxisRaw("Horizontal");
        }

        protected override BaseState GetInitialState()
        {
            return idleState;
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

        #region Animation triggers
        public void AnimationFinishTrigger() { attackState.SetAttackDone(); }

        public void AnimationTurnOffFlipTrigger() { attackState.TurnOffFlip(); }

        public void AnimationTurnOnFlipTrigger() { attackState.TurnOnFlip(); }
        #endregion
    }
}
