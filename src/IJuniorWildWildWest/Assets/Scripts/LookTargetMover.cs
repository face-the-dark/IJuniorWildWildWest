using UnityEngine;

public class LookTargetMover : MonoBehaviour
{
    [SerializeField] private Transform _mainCameraTransform;
    [SerializeField] private float _maxDistance = 100f;

    private void LateUpdate()
    {
        transform.position = _mainCameraTransform.position + _mainCameraTransform.forward * _maxDistance;
    }
}