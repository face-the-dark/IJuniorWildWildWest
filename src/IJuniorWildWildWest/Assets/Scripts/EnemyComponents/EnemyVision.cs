using System;
using PlayerComponents;
using UnityEngine;

namespace EnemyComponents
{
    public class EnemyVision : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private float _viewFieldDistance = 10f;

        public event Action PlayerMissed;

        private void Update()
        {
            Vector3 headPosition =
                new Vector3(transform.position.x, transform.position.y + 1.75f, transform.position.z);
            Vector3 playerHeadPosition =
                new Vector3(_player.position.x, _player.position.y + 1.75f, _player.position.z);

            Vector3 directionToPlayer = (playerHeadPosition - headPosition).normalized;

            Ray ray = new Ray(headPosition, directionToPlayer);

            Physics.Raycast(ray, out RaycastHit hit, _viewFieldDistance);

            if (hit.collider != null 
                && hit.collider.TryGetComponent(out Player player) == false 
                || Vector3.Distance(playerHeadPosition, headPosition) > _viewFieldDistance)
                PlayerMissed?.Invoke();
        }
    }
}