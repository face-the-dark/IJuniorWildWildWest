using Game.Gameplay.Cameras;
using Game.Gameplay.Features;
using Game.Gameplay.Features.Enemies;
using Game.Gameplay.Features.Players;
using Game.Gameplay.Levels;
using Game.Infrastructure.AssetManagement;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IObjectResolver _container;
        private readonly IAssetProvider _assetProvider;
        private readonly ILevelDataProvider _levelDataProvider;
        private readonly PlayerDataProvider _playerDataProvider;

        public GameFactory(IObjectResolver container, IAssetProvider assetProvider,
            ILevelDataProvider levelDataProvider, PlayerDataProvider playerDataProvider)
        {
            _container = container;
            _assetProvider = assetProvider;
            _levelDataProvider = levelDataProvider;
            _playerDataProvider = playerDataProvider;
        }

        public Player CreatePlayer()
        {
            Player player = _assetProvider.LoadAsset<Player>(AssetPath.Player);
            
            _playerDataProvider.Player = player;

            return _container.Instantiate(player, _levelDataProvider.PlayerSpawnPosition, Quaternion.identity);
        }

        public Enemy CreateEnemy(Vector3 spawnPosition)
        {
            Enemy enemy = _assetProvider.LoadAsset<Enemy>(AssetPath.Enemy);

            return _container.Instantiate(enemy, spawnPosition, Quaternion.identity);
        }

        public PlayerCamera CreatePlayerCamera()
        {
            PlayerCamera playerCamera = _assetProvider.LoadAsset<PlayerCamera>(AssetPath.PlayerCamera);

            return _container.Instantiate(playerCamera);
        }

        public PlayerCameraInfo CreatePlayerCameraInfo()
        {
            PlayerCameraInfo playerCameraInfo = _assetProvider.LoadAsset<PlayerCameraInfo>(AssetPath.PlayerCameraInfo);

            _playerDataProvider.PlayerCameraInfo = playerCameraInfo;
            
            return _container.Instantiate(playerCameraInfo);
        }

        public Weapon CreatePlayerWeapon()
        {
            Weapon weapon = _assetProvider.LoadAsset<Weapon>(AssetPath.PlayerRifle);

            _playerDataProvider.Weapon = weapon;
            
            return _container.Instantiate(weapon, _playerDataProvider.Player.RightHand);
        }

        public LookTarget CreateLookTarget()
        {
            LookTarget lookTarget = _assetProvider.LoadAsset<LookTarget>(AssetPath.LookTarget);
            
            _playerDataProvider.LookTarget = lookTarget;

            return _container.Instantiate(lookTarget);
        }
    }
}