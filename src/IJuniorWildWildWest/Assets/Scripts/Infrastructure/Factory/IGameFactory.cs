using EnemyComponents;
using PlayerComponents;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory
    {
        Player CreatePlayer(Vector3 spawnPosition);
        Enemy CreateEnemy(Vector3 spawnPosition, Player player);
    }
}