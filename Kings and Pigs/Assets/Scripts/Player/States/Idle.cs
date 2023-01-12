using UnityEngine;

namespace Player
{
    public class Idle : Grounding
    {
        public Idle(King stateMachine) : base("Idle", stateMachine) { }

        public override void Enter()
        {
            base.Enter();
            animator.SetBool("Moving", false);
        }

        public override void Update()
        {
            base.Update();
            core.SetVelocityX(0f);
            xInput = Input.GetAxisRaw("Horizontal");

            if (Mathf.Abs(xInput) > Mathf.Epsilon)
                stateMachine.ChangeState(player.runningStage);
        }
    }
}