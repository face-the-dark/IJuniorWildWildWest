using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RigSwitcher : MonoBehaviour
{
    private const float RigOffValue = 0f;
    private const float RigOnValue = 1f;
    
    [SerializeField] private Rig _idleRig;
    [SerializeField] private Rig _aimRig;

    private PlayerInputReader _playerInputReader;
    private bool _isAimed;

    private void Awake() => 
        _playerInputReader = GetComponent<PlayerInputReader>();

    private void OnEnable() => 
        _playerInputReader.Aimed += OnAimed;

    private void OnDisable() => 
        _playerInputReader.Aimed -= OnAimed;

    private void OnAimed(bool isAimed)
    {
        _isAimed = isAimed;

        UpdateRigState();
    }
    
    private void UpdateRigState()
    {
        if (_isAimed)
        {
            _idleRig.weight = RigOffValue;
            _aimRig.weight = RigOnValue;
        }
        else
        {
            _idleRig.weight = RigOnValue;
            _aimRig.weight = RigOffValue;
        }
    }
}