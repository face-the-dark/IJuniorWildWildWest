using CameraComponents;
using EnemyComponents;
using PlayerComponents;
using UnityEngine;

namespace Infrastructure
{
    public class GameFactory : MonoBehaviour
    {
        private void Awake() => 
            Initialize();

        private void Initialize()
        {
            Camera mainCamera = CreateMainCamera();
            Player player = CreatePlayer(Vector3.zero, mainCamera);
            
            CreateEnemy(new Vector3(-20f, 0f, -5f), player.transform);
        }

        private Camera CreateMainCamera()
        {
            Camera prefab = Resources.Load<Camera>("Camera/MainCamera");

            return Instantiate(prefab);
        }

        private Player CreatePlayer(Vector3 spawnPosition, Camera mainCamera)
        {
            Player prefab = Resources.Load<Player>("Player/Player");
            Player player = Instantiate(prefab, spawnPosition, Quaternion.identity);
            
            PlayerCamera playerCamera = CreatePlayerCamera(player, player.CameraTarget);
            Weapon weapon = CreatePlayerWeapon(player.RightHand, playerCamera.ShootPoint);
            LookTarget lookTarget = CreateLookTarget(mainCamera.transform);

            player.Construct(mainCamera.transform, weapon, lookTarget.transform);

            return player;
        }

        private Weapon CreatePlayerWeapon(Transform rightHand, Transform shootPoint)
        {
            Weapon prefab = Resources.Load<Weapon>("Player/PlayerRifle");
            Weapon weapon = Instantiate(prefab, rightHand);

            weapon.Construct(shootPoint);

            return weapon;
        }

        private PlayerCamera CreatePlayerCamera(Player player, Transform cameraTarget)
        {
            PlayerCamera prefab = Resources.Load<PlayerCamera>("Camera/PlayerCamera");
            PlayerCamera playerCamera = Instantiate(prefab);

            playerCamera.Construct(player, cameraTarget);

            return playerCamera;
        }

        private Enemy CreateEnemy(Vector3 spawnPosition, Transform player)
        {
            Enemy prefab = Resources.Load<Enemy>("Enemy/Enemy");
            Enemy enemy = Instantiate(prefab, spawnPosition, Quaternion.identity);

            enemy.Construct(player);

            return enemy;
        }
        
        private LookTarget CreateLookTarget(Transform mainCameraTransform)
        {
            LookTarget prefab = Resources.Load<LookTarget>("Camera/LookTarget");
            LookTarget lookTarget = Instantiate(prefab);

            lookTarget.Construct(mainCameraTransform);

            return lookTarget;
        }
    }
}