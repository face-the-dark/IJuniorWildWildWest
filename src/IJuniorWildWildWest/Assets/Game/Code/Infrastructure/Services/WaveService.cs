using System;
using System.Collections.Generic;
using Game.Gameplay.Features.Enemies;
using Game.Gameplay.Features.Players;
using Game.Infrastructure.Factory;
using Game.Infrastructure.States.GameStates;
using Game.Infrastructure.States.StateMachine;
using UnityEngine;

namespace Game.Infrastructure.Services
{
    public class WaveService
    {
        private readonly IGameFactory _factory;
        private readonly IGameStateMachine _stateMachine;
        private readonly List<Transform> _enemySpawnPoints;

        private readonly List<Enemy> _aliveEnemies = new();
        private Player _player;

        private int _currentWave;
        private int _totalWaves = 3;

        public event Action<string> WaveChanged;

        public WaveService(
            IGameFactory factory,
            IGameStateMachine stateMachine,
            List<Transform> enemySpawnPoints)
        {
            _factory = factory;
            _stateMachine = stateMachine;
            _enemySpawnPoints = enemySpawnPoints;
        }

        public void StartWave(Player player)
        {
            _player = player;
            _currentWave = 1;
            SpawnEnemies();
            WaveChanged?.Invoke($"{_currentWave} / {_totalWaves}");
        }

        private void SpawnEnemies()
        {
            _aliveEnemies.Clear();
            
            foreach (Transform point in _enemySpawnPoints)
            {
                Enemy enemy = _factory.CreateEnemy(point.position, _player);
                enemy.Died += OnEnemyDied;
                _aliveEnemies.Add(enemy);
            }
        }

        private void OnEnemyDied(Enemy dead)
        {
            dead.Died -= OnEnemyDied;
            _aliveEnemies.Remove(dead);

            if (_aliveEnemies.Count > 0)
                return;

            if (_currentWave < _totalWaves)
            {
                _currentWave++;
                WaveChanged?.Invoke($"{_currentWave} / {_totalWaves}");
                SpawnEnemies();
            }
            else
            {
                _stateMachine.Enter<WinState, Player>(_player);
            }
        }
    }
}