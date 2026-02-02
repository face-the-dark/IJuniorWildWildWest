using UnityEngine;

namespace PlayerComponents
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PlayerMover))]
    public class PlayerAnimator : MonoBehaviour
    {
        private static readonly int IsRunKey = Animator.StringToHash("IsRun");
        private static readonly int IsAimKey = Animator.StringToHash("IsAim");
        private static readonly int VerticalKey = Animator.StringToHash("Vertical");
        private static readonly int HorizontalKey = Animator.StringToHash("Horizontal");
        private static readonly int FireKey = Animator.StringToHash("Fire");
        private static readonly int HitKey = Animator.StringToHash("Hit");
        private static readonly int DeadKey = Animator.StringToHash("Dead");

        [SerializeField] private PlayerInputReader _inputReader;
        [SerializeField] private PlayerMover _mover;
        [SerializeField] private Health _health;

        private Animator _animator;

        private bool _isRun;
        private Vector3 _velocity;
        private bool _isAimed;

        private void Awake() =>
            _animator = GetComponent<Animator>();

        private void OnEnable()
        {
            _inputReader.Moved += OnMoved;
            _inputReader.Aimed += OnAimed;
            _inputReader.Shoot += OnShoot;

            _mover.NormalizedVelocityChanged += OnNormalizedVelocityChanged;
            
            _health.DamageTaken += OnDamageTaken;
            _health.Died += OnDied;
        }

        private void OnDisable()
        {
            _inputReader.Moved -= OnMoved;
            _inputReader.Aimed -= OnAimed;
            _inputReader.Shoot -= OnShoot;

            _mover.NormalizedVelocityChanged -= OnNormalizedVelocityChanged;
            
            _health.DamageTaken -= OnDamageTaken;
            _health.Died -= OnDied;
        }

        private void OnMoved(Vector2 direction)
        {
            _isRun = direction != Vector2.zero;
            _animator.SetBool(IsRunKey, _isRun);
        }

        private void OnAimed(bool isAimed)
        {
            _isAimed = isAimed;
            _animator.SetBool(IsAimKey, isAimed);
        }

        private void OnShoot()
        {
            if (_isAimed)
                _animator.SetTrigger(FireKey);
        }

        private void OnNormalizedVelocityChanged(Vector3 velocity)
        {
            if (_isRun == false)
            {
                SetParameters(Vector2.zero);
            }
            else
            {
                SetParameters(_isAimed
                    ? new Vector2(velocity.x, velocity.z)
                    : new Vector2(0, Mathf.Clamp(velocity.z, 0, velocity.z))
                );
            }
        }

        private void OnDamageTaken(float healthCurrentValue) => 
            _animator.SetTrigger(HitKey);

        private void OnDied() => 
            _animator.SetTrigger(DeadKey);

        private void SetParameters(Vector2 velocity)
        {
            _animator.SetFloat(HorizontalKey, velocity.x);
            _animator.SetFloat(VerticalKey, velocity.y);
        }
    }
}