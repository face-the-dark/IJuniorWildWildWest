using System.Collections.Generic;
using EnemyComponents;
using Infrastructure;
using UnityEngine;

namespace Spawners
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<Transform> _enemySpawnPoints;
        [SerializeField] private GameFactory _gameFactory;
        [SerializeField] private PlayerSpawner _playerSpawner;

        private List<Enemy> _spawnedEnemies;

        private void Awake() => 
            _spawnedEnemies = new List<Enemy>();

        private void OnEnable() =>
            _playerSpawner.PlayerSpawned += OnPlayerSpawn;

        private void OnDisable() =>
            _playerSpawner.PlayerSpawned -= OnPlayerSpawn;

        private void OnDestroy()
        {
            foreach (Enemy spawnedEnemy in _spawnedEnemies)
            {
                spawnedEnemy.Died -= OnDied;
            }
        }

        private void OnPlayerSpawn(Transform player)
        {
            for (int i = 0; i < _enemySpawnPoints.Count; i++)
                Spawn(_enemySpawnPoints[i].position, player);
        }

        private void OnDied()
        {
            
        }

        private void Spawn(Vector3 spawnPosition, Transform player)
        {
            Enemy enemy = _gameFactory.CreateEnemy(spawnPosition, player);
            _spawnedEnemies.Add(enemy);

            enemy.Died += OnDied;
        }
    }
}