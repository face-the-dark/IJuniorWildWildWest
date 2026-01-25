using UnityEngine;

namespace EnemyComponents
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyMover _mover;
        [SerializeField] private EnemyVision _vision;
        [SerializeField] private ShootPointsCalculator _shootPointsCalculator;
        
        private void OnEnable() => 
            _vision.PlayerMissed += MoveToPlayer;

        private void OnDisable() => 
            _vision.PlayerMissed -= MoveToPlayer;

        private void MoveToPlayer() => 
            _mover.MoveTo(_shootPointsCalculator.CalculateNearShootPosition());
    }
}