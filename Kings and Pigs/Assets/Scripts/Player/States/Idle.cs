using UnityEngine;

namespace Player
{
    public class Idle : Grounding
    {
        public Idle(Player stateMachine) : base("Idle", stateMachine) { }

        public override void Enter()
        {
            base.Enter();
            player.Animator.SetBool("Moving", false);
        }

        public override void Update()
        {
            base.Update();
            core.SetVelocityX(0f);
            xInput = Input.GetAxisRaw("Horizontal");

            if (Mathf.Abs(xInput) > Mathf.Epsilon)
            {
                player.Animator.SetBool("Moving", true);
                stateMachine.ChangeState(player.runningStage);
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

        }
    }
}