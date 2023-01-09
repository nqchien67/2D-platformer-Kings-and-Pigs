using FiniteStateMachine;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class EnterDoor : BaseState
    {
        public EnterDoor(Player player) : base("EnterDoor", player)
        {
        }

        public override void Enter()
        {
            base.Enter();

        }
    }
}