using UnityEngine;

[RequireComponent(typeof(PlayerInputReader))]
[RequireComponent(typeof(CharacterController))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;

    private PlayerInputReader _playerInputReader;
    private CharacterController _characterController;

    private Vector2 _direction;

    private void Awake()
    {
        _playerInputReader = GetComponent<PlayerInputReader>();
        _characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        _playerInputReader.Moved += SetDirection;
    }

    private void OnDisable()
    {
        _playerInputReader.Moved -= SetDirection;
    }

    private void Update()
    {
        Vector3 direction = new Vector3(_direction.x, 0f, _direction.y);
        direction *= _speed * Time.deltaTime;

        _characterController.Move(direction);
    }

    private void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }
}