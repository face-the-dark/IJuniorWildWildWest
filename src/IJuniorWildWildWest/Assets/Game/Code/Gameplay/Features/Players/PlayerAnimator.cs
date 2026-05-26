using UnityEngine;

namespace Game.Gameplay.Features.Players
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        private static readonly int IsRunKey = Animator.StringToHash("IsRun");
        private static readonly int IsAimKey = Animator.StringToHash("IsAim");
        private static readonly int VerticalVelocityKey = Animator.StringToHash("VerticalVelocity");
        private static readonly int HorizontalVelocityKey = Animator.StringToHash("HorizontalVelocity");
        private static readonly int FireKey = Animator.StringToHash("Fire");
        private static readonly int HitKey = Animator.StringToHash("Hit");
        private static readonly int DeadKey = Animator.StringToHash("Dead");
        private static readonly int VictoryKey = Animator.StringToHash("Victory");

        private Animator _animator;
        
        private bool _isRun;
        private Vector3 _velocity;
        private bool _isAimed;

        private void Awake() => 
            _animator = GetComponent<Animator>();

        public void UpdateRun(Vector2 direction)
        {
            _isRun = direction != Vector2.zero;
            _animator.SetBool(IsRunKey, _isRun);
        }

        public void UpdateAim(bool isAimed)
        {
            _isAimed = isAimed;
            _animator.SetBool(IsAimKey, isAimed);
        }

        public void Shoot()
        {
            if (_isAimed)
                _animator.SetTrigger(FireKey);
        }

        public void Hit() =>
            _animator.SetTrigger(HitKey);

        public void Die() =>
            _animator.SetTrigger(DeadKey);

        public void Win() => 
            _animator.SetTrigger(VictoryKey);

        public void UpdateVelocity(Vector3 velocity)
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

        private void SetParameters(Vector2 velocity)
        {
            _animator.SetFloat(HorizontalVelocityKey, velocity.x);
            _animator.SetFloat(VerticalVelocityKey, velocity.y);
        }
    }
}