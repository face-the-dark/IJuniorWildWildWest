using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(PlayerInputReader))]
public class CameraSwitcher : MonoBehaviour
{
    private const int PriorityModifier = 2;

    [SerializeField] private CinemachineVirtualCamera _playerNormalCamera;
    [SerializeField] private CinemachineVirtualCamera _playerAimCamera;
    
    private PlayerInputReader _playerInputReader;

    private void Awake() => 
        _playerInputReader = GetComponent<PlayerInputReader>();

    private void OnEnable() => 
        _playerInputReader.Aimed += OnAimed;

    private void OnDisable() => 
        _playerInputReader.Aimed -= OnAimed;

    private void OnAimed(bool isAimed)
    {
        if (isAimed)
        {
            _playerAimCamera.Priority += PriorityModifier;
            
            _playerAimCamera.transform.position = _playerNormalCamera.transform.position;
            _playerAimCamera.transform.rotation = _playerNormalCamera.transform.rotation;
        }
        else
        {
            _playerAimCamera.Priority -= PriorityModifier;
            
            _playerNormalCamera.transform.position = _playerAimCamera.transform.position;
            _playerNormalCamera.transform.rotation = _playerAimCamera.transform.rotation;
        }
    }
}