using UnityEngine;

namespace PlayerComponents
{
    [RequireComponent(typeof(PlayerInputReader))]
    public class PlayerShooter : MonoBehaviour
    {
        private Weapon _weapon;
        private Transform _mainCamera;

        private bool _isAimed;

        public void Construct(Transform mainCamera, Weapon weapon)
        {
            _mainCamera = mainCamera;
            _weapon = weapon;
        }

        public void SetAimed(bool isAimed) => 
            _isAimed = isAimed;

        public void Shoot()
        {
            if (_isAimed) 
                _weapon.Fire(_mainCamera.forward);
        }
    }
}