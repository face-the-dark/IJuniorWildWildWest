using System;
using UnityEngine;
using VContainer;

namespace Game.Gameplay.Features.Players
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _acceleration = 30f;

        private Rigidbody _rigidbody;
        private DirectionCalculator _directionCalculator;

        private Vector2 _direction;
        private Vector3 _currentVelocity;
        private Vector3 _lastVelocity;

        public event Action<Vector3> NormalizedVelocityChanged;

        [Inject]
        public void Construct(DirectionCalculator directionCalculator)
        {
            _directionCalculator = directionCalculator;
            
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            Vector3 direction = _directionCalculator.CalculateCameraViewDirection(_direction);
            Vector3 targetVelocity = direction * _speed;
            float acceleration = _acceleration * Time.fixedDeltaTime;

            _currentVelocity = Vector3.MoveTowards(_currentVelocity, targetVelocity, acceleration);
            _rigidbody.velocity = new Vector3(_currentVelocity.x, _rigidbody.velocity.y, _currentVelocity.z);

            if (_lastVelocity != _currentVelocity)
            {
                _lastVelocity = _currentVelocity;
                
                Vector3 localVelocity = transform.InverseTransformDirection(_currentVelocity);
                Vector3 normalizedVelocity = localVelocity / _speed;
                
                NormalizedVelocityChanged?.Invoke(normalizedVelocity);
            }
        }

        public void SetDirection(Vector2 direction) =>
            _direction = direction;
    }
}