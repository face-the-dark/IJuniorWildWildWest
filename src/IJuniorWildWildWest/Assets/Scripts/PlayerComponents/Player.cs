using System;
using UnityEngine;

namespace PlayerComponents
{
    [RequireComponent(typeof(PlayerInputReader))]
    [RequireComponent(typeof(PlayerMover))]
    [RequireComponent(typeof(PlayerRotator))]
    [RequireComponent(typeof(PlayerShooter))]
    [RequireComponent(typeof(RigSwitcher))]
    public class Player : MonoBehaviour, IDisposable
    {
        [SerializeField] private Transform _rightHand;
        [SerializeField] private Transform _cameraTarget;

        private PlayerInputReader _inputReader;
        private PlayerMover _mover;
        private PlayerRotator _rotator;
        private PlayerShooter _shooter;
        private DirectionCalculator _directionCalculator;
        private RigSwitcher _rigSwitcher;

        public Transform RightHand => _rightHand;
        public Transform CameraTarget => _cameraTarget;

        public void Construct(Transform mainCamera, Weapon weapon, Transform lookTarget)
        {
            InitializeComponents();
            ConstructComponents(mainCamera, weapon, lookTarget);
        }

        private void InitializeComponents()
        {
            _inputReader = GetComponent<PlayerInputReader>();
            _mover = GetComponent<PlayerMover>();
            _rotator = GetComponent<PlayerRotator>();
            _shooter = GetComponent<PlayerShooter>();
            _rigSwitcher = GetComponent<RigSwitcher>();

            _inputReader.Moved += OnMoved;
            _inputReader.Aimed += OnAimed;
            _inputReader.Shoot += OnShoot;
        }

        public void Dispose()
        {
            _inputReader.Moved -= OnMoved;
            _inputReader.Aimed -= OnAimed;
            _inputReader.Shoot -= OnShoot;
        }

        private void ConstructComponents(Transform mainCamera, Weapon weapon, Transform lookTarget)
        {
            _directionCalculator = new DirectionCalculator(mainCamera);
            
            _mover.Construct(_directionCalculator);
            _rotator.Construct(mainCamera, _directionCalculator);
            _shooter.Construct(mainCamera, weapon);
            _rigSwitcher.Construct(lookTarget);
        }

        private void OnMoved(Vector2 direction)
        {
            _rotator.SetDirection(direction);
            _mover.SetDirection(direction);
        }

        private void OnAimed(bool isAimed)
        {
            _rotator.SetAimed(isAimed);
            _shooter.SetAimed(isAimed);
            _rigSwitcher.UpdateAim(isAimed);
        }

        private void OnShoot()
        {
            _shooter.Shoot();
        }
    }
}