using Cinemachine;
using Game.Gameplay.Features.Players;
using UnityEngine;
using VContainer;

namespace Game.Gameplay.Cameras
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class CameraAimer : MonoBehaviour
    {
        [SerializeField] private float _normalCameraDistance = 2f;
        [SerializeField] private float _normalScreenX = 0.4f;

        [SerializeField] private float _aimCameraDistance = 1.2f;
        [SerializeField] private float _aimScreenX = 0.3f;

        private CinemachineVirtualCamera _mainVirtualCamera;
        private CinemachineVirtualCamera _winVirtualCamera;
        private CinemachineFramingTransposer _virtualCameraBody;

        [Inject]
        public void Construct(PlayerDataProvider playerDataProvider)
        {
            _mainVirtualCamera = GetComponent<CinemachineVirtualCamera>();
            _virtualCameraBody = _mainVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            _winVirtualCamera = playerDataProvider.PlayerCameraInfo.WinVirtualCamera;
            
            _mainVirtualCamera.Follow = playerDataProvider.PlayerCameraInfo.CameraTarget;
            _mainVirtualCamera.LookAt = playerDataProvider.PlayerCameraInfo.CameraTarget;
        }

        public void SetCameraParameters(bool isAimed)
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

        public void SwitchToWinCamera()
        {
            _winVirtualCamera.Priority += 2;
        }
    }
}