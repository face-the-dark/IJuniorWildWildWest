using Game.Gameplay.Features.Enemies;
using Game.Gameplay.Features.Players;
using UnityEngine;

namespace Game.Infrastructure.Factory
{
    public interface IGameFactory
    {
        Player CreatePlayer();
        Enemy CreateEnemy(Vector3 spawnPosition, Player player);
    }
}