using Cinemachine;
using PlayerComponents;
using UnityEngine;

[RequireComponent(typeof(PlayerInputReader))]
public class CameraAimer : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _playerVirtualCamera;
    
    [SerializeField] private float _normalCameraDistance = 2f;
    [SerializeField] private float _normalScreenX = 0.4f;

    [SerializeField] private float _aimCameraDistance = 1.2f;
    [SerializeField] private float _aimScreenX = 0.3f;
    
    private PlayerInputReader _playerInputReader;
    
    private CinemachineFramingTransposer _virtualCameraBody;
    
    private void Awake()
    {
        _playerInputReader = GetComponent<PlayerInputReader>();
        
        _virtualCameraBody = _playerVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    private void OnEnable() => 
        _playerInputReader.Aimed += OnAimed;

    private void OnDisable() => 
        _playerInputReader.Aimed -= OnAimed;

    private void OnAimed(bool isAimed)
    {
        if (isAimed)
            SetCameraParameters(_aimCameraDistance, _aimScreenX);
        else
            SetCameraParameters(_normalCameraDistance, _normalScreenX);
    }

    private void SetCameraParameters(float cameraDistance, float screenX)
    {
        _virtualCameraBody.m_CameraDistance = cameraDistance;
        _virtualCameraBody.m_ScreenX = screenX;
    }
}