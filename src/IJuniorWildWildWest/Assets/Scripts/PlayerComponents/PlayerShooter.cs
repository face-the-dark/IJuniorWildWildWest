using UnityEngine;

namespace PlayerComponents
{
    public class PlayerShooter : MonoBehaviour
    {
        private const float EdgeReduceModifier = 2f;
    
        [SerializeField] private PlayerInputReader _playerInputReader;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private float _damage = 10f;

        private bool _isAimed;
    
        private void OnEnable()
        {
            _playerInputReader.Aimed += OnAimed;
            _playerInputReader.Shoot += Shoot;
        }

        private void OnDisable()
        {
            _playerInputReader.Aimed -= OnAimed;
            _playerInputReader.Shoot -= Shoot;
        }

        private void OnAimed(bool isAimed) => 
            _isAimed = isAimed;

        private void Shoot()
        {
            if (_isAimed)
            {
                Vector3 center = new Vector3(Screen.width / EdgeReduceModifier, Screen.height / EdgeReduceModifier, 0f);
                Ray ray = _mainCamera.ScreenPointToRay(center);

                Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity, _enemyLayer);

                if (hit.collider && hit.collider.TryGetComponent(out Health health)) 
                    health.TakeDamage(_damage);
            }
        }
    }
}