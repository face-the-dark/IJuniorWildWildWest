using Game.Gameplay.Features.FSM;
using UnityEngine;

namespace Game.Gameplay.Features.Enemies.FSM.States
{
    public class DeadState : State
    {
        private Collider _collider;
        
        public override void Enter() => 
            _collider.enabled = false;

        public DeadState(StateMachine stateMachine, Collider collider) : base(stateMachine) => 
            _collider = collider;
    }
}