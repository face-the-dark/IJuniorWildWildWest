using UnityEngine;

[RequireComponent(typeof(PlayerInputReader))]
[RequireComponent(typeof(CharacterController))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private DirectionCalculator _directionCalculator;

    private PlayerInputReader _inputReader;  
    private CharacterController _characterController;
    
    private Vector2 _direction;

    private void Awake()
    {
        _inputReader = GetComponent<PlayerInputReader>();
        _characterController = GetComponent<CharacterController>();
    }
    
    private void OnEnable() => 
        _inputReader.Moved += SetDirection;

    private void OnDisable() => 
        _inputReader.Moved -= SetDirection;

    private void Update()
    {
        Vector3 direction = _directionCalculator.CalculateViewDirection(_direction);
        direction *= _speed * Time.deltaTime;
        
        _characterController.Move(direction);
    }

    private void SetDirection(Vector2 direction) => 
        _direction = direction;
}