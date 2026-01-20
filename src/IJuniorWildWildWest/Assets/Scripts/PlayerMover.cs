using UnityEngine;

[RequireComponent(typeof(PlayerInputReader))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed = 350f;
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
        Vector3 direction = _directionCalculator.CalculateViewDirection(_direction);
        direction *= _speed;
        
        _rigidbody.velocity = new Vector3(direction.x, _rigidbody.velocity.y, direction.z);
    }

    private void SetDirection(Vector2 direction) => 
        _direction = direction;
}