using FSM;

namespace EnemyComponents.FSM.Transitions
{
    public class ToDeadStateTransition : Transition
    {
        private Health _health;
        
        public ToDeadStateTransition(State nextState, Health health) : base(nextState)
        {
            _health = health;
        }

        protected override bool CanTransit()
        {
            return _health.IsDead();
        }
    }
}