using UnityEngine;

namespace PlayerComponents
{
    public class PlayerRotator : MonoBehaviour
    {
        private const float Epsilon = 0.01f;

        [SerializeField] private float _mouseSensitivity = 20f;

        private Transform _mainCamera;
        private DirectionCalculator _directionCalculator;

        private Vector2 _direction;
        private bool _isAimed;
        private bool _isEnabled;

        public void Construct(Transform mainCamera, DirectionCalculator directionCalculator)
        {
            _mainCamera = mainCamera;
            _directionCalculator = directionCalculator;
            
            _isEnabled = true;
        }

        private void LateUpdate()
        {
            if (_isEnabled)
            {
                if (_isAimed)
                    RotateSelf();
                else
                    RotateAroundSelf();
            }
        }

        public void SetDirection(Vector2 direction) =>
            _direction = direction;

        public void SetAimed(bool isAimed) =>
            _isAimed = isAimed;
        
        public void Disable() => 
            _isEnabled = false; 

        private void RotateSelf()
        {
            Quaternion targetRotation = Quaternion.Euler(0f, _mainCamera.eulerAngles.y, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _mouseSensitivity * Time.deltaTime);
        }

        private void RotateAroundSelf()
        {
            Vector3 direction = _directionCalculator.CalculateCameraViewDirection(_direction);

            if (direction.sqrMagnitude > Epsilon)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _mouseSensitivity * Time.deltaTime);
            }
        }
    }
}