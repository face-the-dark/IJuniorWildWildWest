using System;
using Game.Gameplay.Features.Enemies.FSM;
using Game.Gameplay.Features.FSM;
using Game.Gameplay.Features.Players;
using UnityEngine;
using VContainer;

namespace Game.Gameplay.Features.Enemies
{
    [RequireComponent(typeof(EnemyStateMachineFactory))]
    [RequireComponent(typeof(EnemyMover))]
    [RequireComponent(typeof(ShootPointsCalculator))]
    [RequireComponent(typeof(EnemyShooter))]
    [RequireComponent(typeof(EnemyVision))]
    [RequireComponent(typeof(EnemyAnimator))]
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Health))]
    public class Enemy : MonoBehaviour
    {
        private EnemyStateMachineFactory _stateMachineFactory;
        private EnemyMover _enemyMover;
        private ShootPointsCalculator _shootPointsCalculator;
        private EnemyShooter _shooter;
        private EnemyVision _vision;
        private EnemyAnimator _animator;
        private Collider _collider;
        private Health _health;

        private StateMachine _stateMachine;

        private Player _player;

        public event Action<Enemy> Died;

        [Inject]
        public void Construct(Player player)
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
            _collider = GetComponent<Collider>();
            _health = GetComponent<Health>();
            _animator = GetComponent<EnemyAnimator>();

            _health.Died += OnDied;
        }

        private void OnDestroy()
        {
            _health.Died -= OnDied;
        }

        private void ConstructComponents(Player player)
        {
            _shootPointsCalculator.Construct(player.transform);
            _shooter.Construct(player.transform);
            _vision.Construct(player.transform);
            
            _player = player;
        }

        private void CreateStateMachine()
        {
            _stateMachine = _stateMachineFactory.Create
            (
                _enemyMover,
                _shootPointsCalculator,
                _shooter,
                _vision,
                _animator,
                _collider,
                _health,
                _player
            );
        }

        private void OnDied() =>
            Died?.Invoke(this);

        private void Update() =>
            _stateMachine.Update();
    }
}