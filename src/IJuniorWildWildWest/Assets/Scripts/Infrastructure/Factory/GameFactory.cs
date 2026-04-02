using CameraComponents;
using Cinemachine;
using EnemyComponents;
using Infrastructure.AssetManagement;
using PlayerComponents;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly AssetProvider _assetProvider;

        public GameFactory(AssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public Player CreatePlayer(Vector3 spawnPosition)
        {
            Player player = _assetProvider.Instantiate(AssetPath.Player, spawnPosition)
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