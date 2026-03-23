using System;
using Spawners;
using UnityEngine;

public class WaveRunner : MonoBehaviour
{
    [SerializeField] private int _waveCount = 3;
    [SerializeField] private PlayerSpawner _playerSpawner;
    [SerializeField] private EnemySpawner _enemySpawner;

    private Transform _player;
    private int _currentWave;

    public int WaveCount => _waveCount;
    
    public event Action<int> CountIncreased;

    private void Start()
    {
        _currentWave = 1;
        CountIncreased?.Invoke(_currentWave);
    }

    private void OnEnable()
    {
        _playerSpawner.PlayerSpawned += OnPlayerSpawn;
        _enemySpawner.AllEnemiesDied += OnAllEnemiesDied;
    }

    private void OnDisable()
    {
        _playerSpawner.PlayerSpawned -= OnPlayerSpawn;
        _enemySpawner.AllEnemiesDied -= OnAllEnemiesDied;
    }
    
    private void OnPlayerSpawn(Transform player)
    {
        _player = player;
        _enemySpawner.Spawn(player);
    }

    private void OnAllEnemiesDied()
    {
        if (_currentWave < _waveCount)
        {
            _currentWave++;
            _enemySpawner.Spawn(_player);
            
            CountIncreased?.Invoke(_currentWave);
        }
    }
}