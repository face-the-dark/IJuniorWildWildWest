using UnityEngine;

namespace EnemyComponents
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(EnemyMover))]
    public class EnemyAnimator : MonoBehaviour
    {
        private static readonly int IsVelocityKey = Animator.StringToHash("Velocity");

        private Animator _animator;
        private EnemyMover _mover;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _mover = GetComponent<EnemyMover>();
        }

        private void OnEnable() => 
            _mover.VelocityChanged += OnVelocityChanged;

        private void OnDisable() => 
            _mover.VelocityChanged -= OnVelocityChanged;

        private void OnVelocityChanged(float velocity) =>
            _animator.SetFloat(IsVelocityKey, velocity);
    }
}