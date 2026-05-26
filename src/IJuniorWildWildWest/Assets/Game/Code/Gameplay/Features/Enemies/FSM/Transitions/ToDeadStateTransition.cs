using Game.Gameplay.Features.FSM;

namespace Game.Gameplay.Features.Enemies.FSM.Transitions
{
    public class ToDeadStateTransition : Transition
    {
        private readonly Health _health;
        
        public ToDeadStateTransition(State nextState, Health health) : base(nextState) => 
            _health = health;

        protected override bool CanTransit() => 
            _health.IsDead;
    }
}