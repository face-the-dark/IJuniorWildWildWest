using Game.Gameplay.Cameras;
using Game.Gameplay.Features;
using Game.Gameplay.Features.Enemies;
using Game.Gameplay.Features.Players;
using UnityEngine;

namespace Game.Infrastructure.Factory
{
    public interface IGameFactory
    {
        Player CreatePlayer();
        Enemy CreateEnemy(Vector3 spawnPosition);
        PlayerCamera CreatePlayerCamera();
        PlayerCameraInfo CreatePlayerCameraInfo();
        Weapon CreatePlayerWeapon();
        LookTarget CreateLookTarget();
    }
}