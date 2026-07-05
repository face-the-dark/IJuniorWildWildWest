using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Levels
{
    public interface ILevelDataProvider
    {
        Vector3 PlayerSpawnPosition { get; set; }
        List<Vector3> EnemiesSpawnPositions { get; set; }
    }
}