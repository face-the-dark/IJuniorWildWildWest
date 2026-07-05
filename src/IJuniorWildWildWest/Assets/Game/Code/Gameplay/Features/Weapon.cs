using System;
using Game.Gameplay.Cameras;
using Game.Gameplay.Features.Players;
using UnityEngine;
using VContainer;

namespace Game.Gameplay.Features
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private float _damage = 10f;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private LayerMask _targetLayer;
    
        private PlayerCamera _playerCamera;
        
        public event Action Firing;
        public event Action<Vector3> Hit;
    
        [Inject]
        public void Construct(PlayerDataProvider playerDataProvider) => 
            _playerCamera = playerDataProvider.PlayerCamera;

        public void Fire(Vector3 direction)
        {
            Firing?.Invoke();

            Vector3 shootPoint = _shootPoint ? _shootPoint.position : _playerCamera.ShootPoint.position;
            Ray ray = new Ray(shootPoint, direction);
        
            if (Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity, _targetLayer))
            {
                if (hit.collider && hit.collider.TryGetComponent(out Health health))
                {
                    health.TakeDamage(_damage);
                
                    Hit?.Invoke(hit.point);
                }
            }
        }
    }
}