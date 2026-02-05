using CameraComponents;
using EnemyComponents;
using Infrastructure.AssetManagement;
using PlayerComponents;
using UnityEngine;

namespace Infrastructure
{
    public class GameFactory : MonoBehaviour
    {
        private AssetProvider _assetProvider;
        
        private void Awake()
        {
            _assetProvider = new AssetProvider();
            
            Initialize();
        }

        private void Initialize()
        {
            Camera mainCamera = CreateMainCamera();
            Player player = CreatePlayer(Vector3.zero, mainCamera);
            
            CreateEnemy(new Vector3(-20f, 0f, -5f), player.transform);
        }

        private Camera CreateMainCamera()
        {
            return _assetProvider.Instantiate(AssetPath.MainCamera).GetComponent<Camera>();
        }

        private Player CreatePlayer(Vector3 spawnPosition, Camera mainCamera)
        {
            Player player = _assetProvider.Instantiate(AssetPath.Player, spawnPosition).GetComponent<Player>();

            PlayerCamera playerCamera = CreatePlayerCamera(player, player.CameraTarget);
            Weapon weapon = CreatePlayerWeapon(player.RightHand, playerCamera.ShootPoint);
            LookTarget lookTarget = CreateLookTarget(mainCamera.transform);

            player.Construct(mainCamera.transform, weapon, lookTarget.transform);

            return player;
        }

        private PlayerCamera CreatePlayerCamera(Player player, Transform cameraTarget)
        {
            PlayerCamera playerCamera = _assetProvider.Instantiate(AssetPath.PlayerCamera).GetComponent<PlayerCamera>();

            playerCamera.Construct(player, cameraTarget);

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

        private Enemy CreateEnemy(Vector3 spawnPosition, Transform player)
        {
            Enemy enemy = _assetProvider.Instantiate(AssetPath.Enemy, spawnPosition).GetComponent<Enemy>();

            enemy.Construct(player);

            return enemy;
        }
    }
}