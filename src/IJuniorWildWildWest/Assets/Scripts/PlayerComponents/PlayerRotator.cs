using UnityEngine;

namespace PlayerComponents
{
    [RequireComponent(typeof(PlayerInputReader))]
    [RequireComponent(typeof(DirectionCalculator))]
    public class PlayerRotator : MonoBehaviour
    {
        private const float Epsilon = 0.00001f;

        [SerializeField] private float _speed = 20f;

        private Camera _mainCamera;
        private PlayerInputReader _inputReader;
        private DirectionCalculator _directionCalculator;

        private Vector2 _direction;
        private bool _isAimed;

        public void Construct(Camera mainCamera) => 
            _mainCamera = mainCamera;

        private void Awake()
        {
            _inputReader = GetComponent<PlayerInputReader>();
            _directionCalculator =  GetComponent<DirectionCalculator>();
        }

        private void OnEnable()
        {
            _inputReader.Moved += SetDirection;
            _inputReader.Aimed += SetAimed;
        }

        private void OnDisable()
        {
            _inputReader.Moved -= SetDirection;
            _inputReader.Aimed -= SetAimed;
        }

        private void Update()
        {
            if (_isAimed)
                RotateSelf();
            else
                RotateAroundSelf();
        }

        private void RotateSelf()
        {
            Quaternion targetRotation = Quaternion.Euler(0f, _mainCamera.transform.eulerAngles.y, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _speed * Time.deltaTime);
        }

        private void RotateAroundSelf()
        {
            Vector3 direction = _directionCalculator.CalculateCameraViewDirection(_direction);

            if (direction.sqrMagnitude > Epsilon)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _speed * Time.deltaTime);
            }
        }

        private void SetDirection(Vector2 direction) =>
            _direction = direction;

        private void SetAimed(bool isAimed) =>
            _isAimed = isAimed;
    }
}