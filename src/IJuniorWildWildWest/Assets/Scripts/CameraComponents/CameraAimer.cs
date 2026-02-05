using Cinemachine;
using PlayerComponents;
using UnityEngine;

namespace CameraComponents
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class CameraAimer : MonoBehaviour
    {
        [SerializeField] private float _normalCameraDistance = 2f;
        [SerializeField] private float _normalScreenX = 0.4f;

        [SerializeField] private float _aimCameraDistance = 1.2f;
        [SerializeField] private float _aimScreenX = 0.3f;

        private PlayerInputReader _playerInputReader;

        private CinemachineFramingTransposer _virtualCameraBody;

        public void Construct(Player player, Transform cameraTarget)
        {
            _playerInputReader = player.GetComponent<PlayerInputReader>();
            
            CinemachineVirtualCamera playerVirtualCamera = GetComponent<CinemachineVirtualCamera>();
            _virtualCameraBody = playerVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            
            playerVirtualCamera.Follow = cameraTarget;
            playerVirtualCamera.LookAt = cameraTarget;
            
            _playerInputReader.Aimed += OnAimed;
        }

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
}