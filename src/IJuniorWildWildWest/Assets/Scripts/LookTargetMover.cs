using UnityEngine;

public class LookTargetMover : MonoBehaviour
{
    [SerializeField] private Transform _mainCameraTransform;
    [SerializeField] private float _maxDistance = 100f;
    
    private void LateUpdate()
    {
        if (Physics.Raycast(_mainCameraTransform.position, _mainCameraTransform.forward, out RaycastHit hit, _maxDistance))
            transform.position = hit.point;
        else
            transform.position = _mainCameraTransform.position + _mainCameraTransform.forward * _maxDistance;
    }
}