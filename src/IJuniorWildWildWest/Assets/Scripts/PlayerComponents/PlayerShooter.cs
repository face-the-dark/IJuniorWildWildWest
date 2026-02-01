using UnityEngine;

namespace PlayerComponents
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private PlayerInputReader _playerInputReader;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Weapon _weapon;

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
                _weapon.Fire(_mainCamera.transform.forward);
            }
        }
    }
}