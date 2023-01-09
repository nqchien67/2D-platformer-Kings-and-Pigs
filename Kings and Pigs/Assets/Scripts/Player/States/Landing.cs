using UnityEngine;
namespace Player
{
    public class Landing : Grounding
    {
        public Landing(Player stateMachine) : base("Landing", stateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            player.Animator.SetBool("Grounded", true);
        }

        public override void Update()
        {
            base.Update();
            if (xInput != 0)
            {
                player.ChangeState(player.runningStage);
            }
            else if (Mathf.Abs(xInput) < Mathf.Epsilon)
            { player.ChangeState(player.idleStage); }
        }
    }
}
