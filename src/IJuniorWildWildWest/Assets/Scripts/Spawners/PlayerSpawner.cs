using System;
using Infrastructure;
using Infrastructure.Factory;
using PlayerComponents;
using UnityEngine;

namespace Spawners
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private GameFactory _gameFactory;
        
        public event Action<Player> PlayerSpawned;

        private void Start() => 
            Spawn();

        private void Spawn()
        {
            Player player = _gameFactory.CreatePlayer(_playerSpawnPoint.position);

            PlayerSpawned?.Invoke(player);
        }
    }
}