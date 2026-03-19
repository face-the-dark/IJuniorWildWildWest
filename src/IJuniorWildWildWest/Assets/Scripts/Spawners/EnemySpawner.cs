using System;
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

        private List<Enemy> _spawnedEnemies;

        public event Action AllEnemiesDied;

        private void Awake() => 
            _spawnedEnemies = new List<Enemy>();
        
        public void Spawn(Transform player)
        {
            for (int i = 0; i < _enemySpawnPoints.Count; i++)
                Spawn(_enemySpawnPoints[i].position, player);
        }

        private void Spawn(Vector3 spawnPosition, Transform player)
        {
            Enemy enemy = _gameFactory.CreateEnemy(spawnPosition, player);
            enemy.Died += OnDied;
            _spawnedEnemies.Add(enemy);
        }

        private void OnDied(Enemy enemy)
        {
            enemy.Died -= OnDied;
            _spawnedEnemies.Remove(enemy);

            if (_spawnedEnemies.Count == 0) 
                AllEnemiesDied?.Invoke();
        }
    }
}