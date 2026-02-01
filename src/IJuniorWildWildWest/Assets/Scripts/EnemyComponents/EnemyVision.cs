using System;
using PlayerComponents;
using UnityEngine;

namespace EnemyComponents
{
    public class EnemyVision : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private float _viewFieldDistance = 10f;
        [SerializeField] private float _distanceEpsilon = 1f;

        public event Action PlayerMissed;

        private void Update()
        {
            Vector3 position =
                new Vector3(transform.position.x, transform.position.y + 1.75f, transform.position.z);
            Vector3 playerPosition =
                new Vector3(_player.position.x, _player.position.y + 1.75f, _player.position.z);

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

            if (hasPlayerComponent == false || distance > _viewFieldDistance + _distanceEpsilon)
                PlayerMissed?.Invoke();
        }
    }
}