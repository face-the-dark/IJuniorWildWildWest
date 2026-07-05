using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Levels
{
    public class LevelDataProvider : ILevelDataProvider
    {
        public Vector3 PlayerSpawnPosition { get; set; }
        public List<Vector3> EnemiesSpawnPositions { get; set; }
    }
}