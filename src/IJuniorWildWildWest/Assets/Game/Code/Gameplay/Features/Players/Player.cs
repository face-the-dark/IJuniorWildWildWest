using Cinemachine;
using Game.Gameplay.Cameras;
using UnityEngine;
using VContainer;

namespace Game.Gameplay.Features.Players
{
    [RequireComponent(typeof(PlayerInputReader))]
    [RequireComponent(typeof(PlayerMover))]
    [RequireComponent(typeof(PlayerRotator))]
    [RequireComponent(typeof(PlayerShooter))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(RigSwitcher))]
    [RequireComponent(typeof(PlayerAnimator))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform _rightHand;

        private PlayerInputReader _inputReader;
        private PlayerMover _mover;
        private PlayerRotator _rotator;
        private PlayerShooter _shooter;
        private Health _health;

        private RigSwitcher _rigSwitcher;
        private PlayerAnimator _animator;

        public Transform RightHand => _rightHand;

        public bool IsDead => _health.IsDead;
        
        [Inject]
        public void Construct()
        {
            InitializeComponents();
            SubscribeToComponentsEvents();
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

        public void Win()
        {
            _animator.Win();
            _inputReader.Disable();
            _rotator.Disable();
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