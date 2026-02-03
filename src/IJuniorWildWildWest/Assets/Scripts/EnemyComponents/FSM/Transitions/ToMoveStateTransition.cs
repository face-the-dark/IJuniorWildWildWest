using FSM;

namespace EnemyComponents.FSM.Transitions
{
    public class ToMoveStateTransition : Transition
    {
        private readonly EnemyVision _vision;

        public ToMoveStateTransition(State nextState, EnemyVision vision) : base(nextState) => 
            _vision = vision;

        protected override bool CanTransit() => 
            _vision.IsMissedPlayer();
    }
}