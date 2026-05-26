using Game.Gameplay.Features.Players;
using UnityEngine;

namespace Game.Gameplay.Features.Enemies
{
    public class EnemyVision : MonoBehaviour
    {
        private const float HeadHeightOffset = 1.75f;

        [SerializeField] private float _viewFieldDistance = 10f;
        [SerializeField] private float _distanceEpsilon = 1f;
        
        private Transform _player;

        public void Construct(Transform player) => 
            _player = player;

        public bool IsMissedPlayer()
        {
            Vector3 position =
                new Vector3(transform.position.x, transform.position.y + HeadHeightOffset, transform.position.z);
            Vector3 playerPosition =
                new Vector3(_player.position.x, _player.position.y + HeadHeightOffset, _player.position.z);

            Vector3 directionToPlayer = playerPosition - position;

            Ray ray = new Ray(position, directionToPlayer);

            Physics.Raycast(ray, out RaycastHit hit, _viewFieldDistance);

            Collider hitInfoCollider = hit.collider;

            bool hasPlayerComponent = false;
            
            if (hitInfoCollider)
            {
                hasPlayerComponent = hitInfoCollider.TryGetComponent(out Player player);
            }
            
            float distance = Vector3.Distance(playerPosition, position);

            return hasPlayerComponent == false || distance > _viewFieldDistance + _distanceEpsilon;
        }
    }
}