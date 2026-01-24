using UnityEngine;

[RequireComponent(typeof(PlayerInputReader))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private DirectionCalculator _directionCalculator;

    private PlayerInputReader _inputReader;
    private Rigidbody _rigidbody;
    
    private Vector2 _direction;

    private void Awake()
    {
        _inputReader = GetComponent<PlayerInputReader>();
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    private void OnEnable() => 
        _inputReader.Moved += SetDirection;

    private void OnDisable() => 
        _inputReader.Moved -= SetDirection;

    private void FixedUpdate()
    {
        Vector3 direction = _directionCalculator.CalculateCameraViewDirection(_direction);
        direction *= _speed;
        
        Vector3 velocity = new Vector3(direction.x, _rigidbody.velocity.y, direction.z);
        _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, velocity, _speed);
    }

    private void SetDirection(Vector2 direction) => 
        _direction = direction;
}