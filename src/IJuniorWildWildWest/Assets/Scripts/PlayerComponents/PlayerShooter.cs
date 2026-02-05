using UnityEngine;

namespace PlayerComponents
{
    [RequireComponent(typeof(PlayerInputReader))]
    public class PlayerShooter : MonoBehaviour
    {
        private Weapon _weapon;
        private Camera _mainCamera;
        private PlayerInputReader _playerInputReader;

        private bool _isAimed;

        public void Construct(Camera mainCamera, Weapon weapon)
        {
            _mainCamera = mainCamera;
            _weapon = weapon;
        }

        private void Awake() => 
            _playerInputReader = GetComponent<PlayerInputReader>();

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
                _weapon.Fire(_mainCamera.transform.forward);
        }
    }
}