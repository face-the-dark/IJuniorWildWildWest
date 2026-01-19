using UnityEngine;

public abstract class PlayerTransformer : MonoBehaviour
{
    [SerializeField] protected float Speed = 10f;
    [SerializeField] protected Camera Camera;

    protected PlayerInputReader InputReader;   
    
    private Vector2 _direction;

    protected virtual void Awake() => 
        InputReader = GetComponent<PlayerInputReader>();

    protected virtual void OnEnable() => 
        InputReader.Moved += SetDirection;

    protected virtual void OnDisable() => 
        InputReader.Moved -= SetDirection;

    protected virtual void Update()
    {
        Vector3 direction = new Vector3(_direction.x, 0f, _direction.y);

        Vector3 cameraForward = Camera.transform.forward;
        Vector3 cameraRight = Camera.transform.right;
        
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        
        direction = cameraForward.normalized * direction.z + cameraRight.normalized * direction.x;

        Transform(direction);
    }

    private void SetDirection(Vector2 direction) => 
        _direction = direction;

    protected abstract void Transform(Vector3 direction);
}