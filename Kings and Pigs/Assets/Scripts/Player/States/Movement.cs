using FiniteStateMachine;
using UnityEngine;

namespace Player
{
    public class Movement : BaseState
    {
        protected Player player;
        protected Core core;
        protected PlayerData data;
        protected Rigidbody2D rigidbody;
        protected float xInput;

        public Movement(string name, Player player) : base(name, player)
        {
            this.player = player;
            core = player.core;
            data = player.data;
            rigidbody = player.Rigidbody;
        }

        public override void Update()
        {
            base.Update();

            xInput = player.xInput;

            if (Input.GetKeyDown(KeyCode.S) && !core.isKnockbacking)
                stateMachine.ChangeState(player.attackStage);
        }
    }
}
