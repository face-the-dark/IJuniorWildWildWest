using UnityEngine;

public abstract class PlayerTransformer : MonoBehaviour
{
    [SerializeField] protected float Speed = 10f;
    
    [SerializeField] private Camera _camera;
    
    private PlayerInputReader _inputReader;   
    
    private Vector2 _direction;

    protected virtual void Awake()
    {
        _inputReader = GetComponent<PlayerInputReader>();
    }

    private void OnEnable()
    {
        _inputReader.Moved += SetDirection;
    }

    private void OnDisable()
    {
        _inputReader.Moved -= SetDirection;
    }
    
    private void Update()
    {
        Vector3 direction = new Vector3(_direction.x, 0f, _direction.y);

        Vector3 cameraForward = _camera.transform.forward;
        Vector3 cameraRight = _camera.transform.right;
        
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        
        direction = cameraForward.normalized * direction.z + cameraRight.normalized * direction.x;

        Transform(direction);
    }
    
    private void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    protected abstract void Transform(Vector3 direction);
}