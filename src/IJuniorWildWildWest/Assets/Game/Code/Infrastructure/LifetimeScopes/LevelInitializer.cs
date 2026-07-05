using System.Collections.Generic;
using System.Linq;
using Game.Gameplay.Cameras.Provider;
using Game.Gameplay.Levels;
using UnityEngine;
using VContainer;

namespace Game.Infrastructure.LifetimeScopes
{
    public class LevelInitializer : MonoBehaviour
    {
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private List<Transform> _enemiesSpawnPoints;
        [SerializeField] private Camera _mainCamera;

        private ILevelDataProvider _levelDataProvider;
        private ICameraProvider _cameraProvider;
        
        [Inject]
        public void Construct(ILevelDataProvider levelDataProvider, ICameraProvider cameraProvider)
        {
            _levelDataProvider = levelDataProvider;
            _cameraProvider = cameraProvider;
            
            _levelDataProvider.PlayerSpawnPosition = _playerSpawnPoint.position;
            _levelDataProvider.EnemiesSpawnPositions = _enemiesSpawnPoints
                .Select(x => x.position)
                .ToList();
            
            _cameraProvider.MainCamera = _mainCamera;
        }
    }
}