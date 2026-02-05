using FSM;

namespace EnemyComponents.FSM.States
{
    public class MoveState : State
    {
        private readonly EnemyMover _mover;
        private readonly ShootPointsCalculator _shootPointsCalculator;

        public MoveState(
            StateMachine stateMachine,
            EnemyMover mover,
            ShootPointsCalculator shootPointsCalculator
        ) : base(stateMachine)
        {
            _mover = mover;
            _shootPointsCalculator = shootPointsCalculator;
        }

        public override void Enter() => 
            _mover.MoveTo(_shootPointsCalculator.CalculateNearShootPosition());
    }
}