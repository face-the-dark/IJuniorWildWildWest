using FSM;

namespace EnemyComponents.FSM.Transitions
{
    public class ToAttackStateTransition : Transition
    {
        private readonly EnemyMover _mover;

        public ToAttackStateTransition(State nextState, EnemyMover mover) : base(nextState) => 
            _mover = mover;

        protected override bool CanTransit() => 
            _mover.IsArrivedAtShootPoint();
    }
}