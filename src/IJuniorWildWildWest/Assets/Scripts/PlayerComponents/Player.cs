using UnityEngine;

namespace PlayerComponents
{
    [RequireComponent(typeof(PlayerRotator))]
    [RequireComponent(typeof(DirectionCalculator))]
    [RequireComponent(typeof(PlayerShooter))]
    [RequireComponent(typeof(RigSwitcher))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform _rightHand;
        [SerializeField] private Transform _cameraTarget;
        
        private PlayerRotator _playerRotator;
        private PlayerShooter _playerShooter;
        private DirectionCalculator _directionCalculator;
        private RigSwitcher _rigSwitcher;

        public Transform RightHand => _rightHand;
        public Transform CameraTarget => _cameraTarget;

        public void Construct(Camera mainCamera, Weapon weapon, Transform lookTarget)
        {
            InitializeComponents();
            ConstructComponents(mainCamera, weapon, lookTarget);
        }

        private void InitializeComponents()
        {
            _playerRotator = GetComponent<PlayerRotator>();
            _directionCalculator = GetComponent<DirectionCalculator>();
            _playerShooter = GetComponent<PlayerShooter>();
            _rigSwitcher = GetComponent<RigSwitcher>();
        }

        private void ConstructComponents(Camera mainCamera, Weapon weapon, Transform lookTarget)
        {
            _playerRotator.Construct(mainCamera);
            _playerShooter.Construct(mainCamera, weapon);
            _directionCalculator.Construct(mainCamera);
            _rigSwitcher.Construct(lookTarget);
        }
    }
}