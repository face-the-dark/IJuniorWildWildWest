using UnityEngine;
using UnityEngine.Animations.Rigging;

public class LookTargetMover : MonoBehaviour
{
    [SerializeField] private Rig _idleRig;
    [SerializeField] private Rig _aimRig;
    [SerializeField] private PlayerInputReader _inputReader;
    [SerializeField] private Transform _mainCameraTransform;
    [SerializeField] private Transform _lookTarget;
    [SerializeField] private float _maxDistance = 100f;
    [SerializeField] private LayerMask mask;

    private bool _isAimed;
    
    private void OnEnable()
    {
        _inputReader.Aimed += OnAimed;
    }

    private void OnDisable()
    {
        _inputReader.Aimed -= OnAimed;
    }

    private void OnAimed(bool isAimed)
    {
        _isAimed = isAimed;

        UpdateRigState();
    }

    private void UpdateRigState()
    {
        if (_isAimed)
        {
            _idleRig.weight = 0f;
            _aimRig.weight = 1f;
        }
        else
        {
            _idleRig.weight = 1f;
            _aimRig.weight = 0f;
        }
    }


    private void LateUpdate()
    {
        Ray ray = new Ray(_mainCameraTransform.position, _mainCameraTransform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, _maxDistance, mask))
            _lookTarget.position = hit.point;
        else
            _lookTarget.position = _mainCameraTransform.position + _mainCameraTransform.forward * _maxDistance;
    }
}