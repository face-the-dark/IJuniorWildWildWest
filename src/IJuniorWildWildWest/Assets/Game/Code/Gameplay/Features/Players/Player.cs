using Cinemachine;
using Game.Gameplay.Cameras;
using UnityEngine;

namespace Game.Gameplay.Features.Players
{
    [RequireComponent(typeof(PlayerInputReader))]
    [RequireComponent(typeof(PlayerMover))]
    [RequireComponent(typeof(PlayerRotator))]
    [RequireComponent(typeof(PlayerShooter))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(RigSwitcher))]
    [RequireComponent(typeof(PlayerAnimator))]
    [RequireComponent(typeof(PlayerAnimator))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _winVirtualCamera;
        [SerializeField] private Transform _rightHand;
        [SerializeField] private Transform _cameraTarget;

        private PlayerInputReader _inputReader;
        private PlayerMover _mover;
        private PlayerRotator _rotator;
        private PlayerShooter _shooter;
        private PlayerCamera _playerCamera;
        private Health _health;
        private DirectionCalculator _directionCalculator;

        private RigSwitcher _rigSwitcher;
        private PlayerAnimator _animator;

        public Transform RightHand => _rightHand;
        public Transform CameraTarget => _cameraTarget;

        public bool IsDead => _health.IsDead;
        public CinemachineVirtualCamera WinVirtualCamera => _winVirtualCamera;

        public void Construct(Transform mainCamera, Weapon weapon, Transform lookTarget, PlayerCamera playerCamera)
        {
            InitializeComponents();
            SubscribeToComponentsEvents();
            ConstructComponents(mainCamera, weapon, lookTarget);
            
            _playerCamera = playerCamera;
        }

        private void InitializeComponents()
        {
            _inputReader = GetComponent<PlayerInputReader>();
            _mover = GetComponent<PlayerMover>();
            _rotator = GetComponent<PlayerRotator>();
            _shooter = GetComponent<PlayerShooter>();
            _health = GetComponent<Health>();
            _rigSwitcher = GetComponent<RigSwitcher>();
            _animator = GetComponent<PlayerAnimator>();
        }

        private void SubscribeToComponentsEvents()
        {
            _inputReader.Moved += OnMoved;
            _inputReader.Aimed += OnAimed;
            _inputReader.Shoot += OnShoot;

            _mover.NormalizedVelocityChanged += OnNormalizedVelocityChanged;
            
            _health.DamageTaken += OnDamageTaken;
            _health.Died += OnDied;
        }

        public void OnDestroy() => 
            UnsubscribeFromComponentsEvents();

        private void UnsubscribeFromComponentsEvents()
        {
            _inputReader.Moved -= OnMoved;
            _inputReader.Aimed -= OnAimed;
            _inputReader.Shoot -= OnShoot;
            
            _mover.NormalizedVelocityChanged -= OnNormalizedVelocityChanged;
            
            _health.DamageTaken -= OnDamageTaken;
            _health.Died -= OnDied;
        }

        private void ConstructComponents(Transform mainCamera, Weapon weapon, Transform lookTarget)
        {
            _directionCalculator = new DirectionCalculator(mainCamera);
            
            _mover.Construct(_directionCalculator);
            _rotator.Construct(mainCamera, _directionCalculator);
            _shooter.Construct(mainCamera, weapon);
            _rigSwitcher.Construct(lookTarget);
        }

        public void Win()
        {
            _animator.Win();
            _inputReader.Disable();
            _rotator.Disable();
            _playerCamera.SwitchToWinCamera();
        }

        private void OnMoved(Vector2 direction)
        {
            _rotator.SetDirection(direction);
            _mover.SetDirection(direction);
            _animator.UpdateRun(direction);
        }

        private void OnAimed(bool isAimed)
        {
            _rotator.SetAimed(isAimed);
            _shooter.SetAimed(isAimed);
            _rigSwitcher.UpdateAim(isAimed);
            _animator.UpdateAim(isAimed);
            _playerCamera.SetCameraParameters(isAimed);
        }

        private void OnShoot()
        {
            _shooter.Shoot();
            _animator.Shoot();
        }

        private void OnNormalizedVelocityChanged(Vector3 velocity) => 
            _animator.UpdateVelocity(velocity);

        private void OnDamageTaken(float currentHealthValue) => 
            _animator.Hit();

        private void OnDied()
        {
            _animator.Die();
            _inputReader.Disable();
        }
    }
}