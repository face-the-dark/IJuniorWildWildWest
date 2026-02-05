using UnityEngine;

public class LookTarget : MonoBehaviour
{
    [SerializeField] private float _maxDistance = 100f;
    
    private Transform _mainCameraTransform;

    public void Construct(Transform mainCameraTransform) => 
        _mainCameraTransform = mainCameraTransform;

    private void LateUpdate() => 
        transform.position = _mainCameraTransform.position + _mainCameraTransform.forward * _maxDistance;
}