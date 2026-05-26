using Cinemachine;
using Game.Gameplay.Cameras;
using Game.Gameplay.Features;
using Game.Gameplay.Features.Enemies;
using Game.Gameplay.Features.Players;
using Game.Infrastructure.AssetManagement;
using UnityEngine;

namespace Game.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly AssetProvider _assetProvider;

        private Transform _playerSpawnPoint;

        public GameFactory(AssetProvider assetProvider, Transform playerSpawnPoint)
        {
            _assetProvider = assetProvider;
            _playerSpawnPoint = playerSpawnPoint;
        }

        public Player CreatePlayer()
        {
            Player player = _assetProvider.Instantiate(AssetPath.Player, _playerSpawnPoint)
                .GetComponent<Player>();

            Camera mainCamera = CreateMainCamera();
            PlayerCamera playerCamera = CreatePlayerCamera(player.CameraTarget, player.WinVirtualCamera);
            Weapon weapon = CreatePlayerWeapon(player.RightHand, playerCamera.ShootPoint);
            LookTarget lookTarget = CreateLookTarget(mainCamera.transform);

            player.Construct(mainCamera.transform, weapon, lookTarget.transform, playerCamera);

            return player;
        }

        public Enemy CreateEnemy(Vector3 spawnPosition, Player player)
        {
            Enemy enemy = _assetProvider.Instantiate(AssetPath.Enemy, spawnPosition).GetComponent<Enemy>();

            enemy.Construct(player);

            return enemy;
        }

        private Camera CreateMainCamera()
        {
            return _assetProvider.Instantiate(AssetPath.MainCamera).GetComponent<Camera>();
        }

        private PlayerCamera CreatePlayerCamera(Transform cameraTarget, CinemachineVirtualCamera winVirtualCamera)
        {
            PlayerCamera playerCamera = _assetProvider.Instantiate(AssetPath.PlayerCamera).GetComponent<PlayerCamera>();

            playerCamera.Construct(cameraTarget, winVirtualCamera);

            return playerCamera;
        }

        private Weapon CreatePlayerWeapon(Transform rightHand, Transform shootPoint)
        {
            Weapon weapon = _assetProvider.Instantiate(AssetPath.PlayerRifle, rightHand).GetComponent<Weapon>();

            weapon.Construct(shootPoint);

            return weapon;
        }

        private LookTarget CreateLookTarget(Transform mainCameraTransform)
        {
            LookTarget lookTarget = _assetProvider.Instantiate(AssetPath.LookTarget).GetComponent<LookTarget>();

            lookTarget.Construct(mainCameraTransform);

            return lookTarget;
        }
    }
}