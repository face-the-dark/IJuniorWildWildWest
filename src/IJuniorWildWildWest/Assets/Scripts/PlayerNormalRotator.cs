using UnityEngine;

public class PlayerNormalRotator : PlayerTransformer
{
    [SerializeField] private float _epsilon = 0.01f;

    private bool _isAimed;
    
    protected override void OnEnable()
    {
        base.OnEnable();

        InputReader.Aimed += SetAimed;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        
        InputReader.Aimed -= SetAimed;
    }

    protected override void Transform(Vector3 direction)
    {
        if (_isAimed)
            RotateSelf();
        else
            RotateAroundSelf(direction);
    }

    private void RotateSelf()
    {
        Quaternion targetRotation = Quaternion.Euler(0f, Camera.transform.eulerAngles.y, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Speed * Time.deltaTime);
    }

    private void RotateAroundSelf(Vector3 direction)
    {
        if (direction.sqrMagnitude > _epsilon)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Speed * Time.deltaTime);
        }
    }

    private void SetAimed(bool isAimed) => 
        _isAimed = isAimed;
}