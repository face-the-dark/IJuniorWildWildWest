using UnityEngine;

namespace Game.Gameplay.Features.Enemies
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(EnemyMover))]
    [RequireComponent(typeof(Health))]
    public class EnemyAnimator : MonoBehaviour
    {
        private static readonly int IsVelocityKey = Animator.StringToHash("Velocity");
        private static readonly int HitKey = Animator.StringToHash("Hit");
        private static readonly int DeadKey = Animator.StringToHash("Dead");
        private static readonly int VictoryKey = Animator.StringToHash("Victory");

        private Animator _animator;
        private EnemyMover _mover;
        private Health _health;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _mover = GetComponent<EnemyMover>();
            _health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            _mover.VelocityChanged += OnVelocityChanged;
            
            _health.DamageTaken += OnDamageTaken;
            _health.Died += OnDied;
        }

        private void OnDisable()
        {
            _mover.VelocityChanged -= OnVelocityChanged;
            
            _health.DamageTaken -= OnDamageTaken;
            _health.Died -= OnDied;
        }

        public void PlayVictory()
        {
            _animator.SetTrigger(VictoryKey);
        }

        private void OnVelocityChanged(float velocity) =>
            _animator.SetFloat(IsVelocityKey, velocity);

        private void OnDamageTaken(float healthCurrentValue) => 
            _animator.SetTrigger(HitKey);

        private void OnDied() => 
            _animator.SetTrigger(DeadKey);
    }
}