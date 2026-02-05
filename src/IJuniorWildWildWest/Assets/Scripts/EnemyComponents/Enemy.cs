using EnemyComponents.FSM;
using FSM;
using UnityEngine;

namespace EnemyComponents
{
    [RequireComponent(typeof(EnemyStateMachineFactory))]
    [RequireComponent(typeof(EnemyMover))]
    [RequireComponent(typeof(ShootPointsCalculator))]
    [RequireComponent(typeof(EnemyShooter))]
    [RequireComponent(typeof(EnemyVision))]
    [RequireComponent(typeof(Health))]
    public class Enemy : MonoBehaviour
    {
        private EnemyStateMachineFactory _stateMachineFactory;
        private EnemyMover _enemyMover;
        private ShootPointsCalculator _shootPointsCalculator;
        private EnemyShooter _shooter;
        private EnemyVision _vision;
        private Health _health;

        private StateMachine _stateMachine;

        public void Construct(Transform player)
        {
            InitializeComponents();
            ConstructComponents(player);
            CreateStateMachine();
        }

        private void InitializeComponents()
        {
            _stateMachineFactory = GetComponent<EnemyStateMachineFactory>();
            _enemyMover = GetComponent<EnemyMover>();
            _shootPointsCalculator = GetComponent<ShootPointsCalculator>();
            _shooter = GetComponent<EnemyShooter>();
            _vision = GetComponent<EnemyVision>();
            _health = GetComponent<Health>();
        }

        private void ConstructComponents(Transform player)
        {
            _shootPointsCalculator.Construct(player);
            _shooter.Construct(player);
            _vision.Construct(player);
        }

        private void CreateStateMachine()
        {
            _stateMachine = _stateMachineFactory.Create
            (
                _enemyMover,
                _shootPointsCalculator,
                _shooter,
                _vision,
                _health
            );
        }

        private void Update() => 
            _stateMachine.Update();
    }
}