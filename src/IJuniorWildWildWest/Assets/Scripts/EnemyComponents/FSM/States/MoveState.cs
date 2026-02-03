using FSM;
using UnityEngine;

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

        public override void Enter()
        {
            Vector3 calculateNearShootPosition = _shootPointsCalculator.CalculateNearShootPosition();
            _mover.MoveTo(calculateNearShootPosition);
        }
    }
}