using Cinemachine;
using UnityEngine;

namespace CameraComponents
{
    [RequireComponent(typeof(CameraAimer))]
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;
        
        private CameraAimer _cameraAimer;

        public Transform ShootPoint => _shootPoint;
        
        public void Construct(Transform cameraTarget, CinemachineVirtualCamera winVirtualCamera)
        {
            _cameraAimer = GetComponent<CameraAimer>();
            _cameraAimer.Construct(cameraTarget, winVirtualCamera);
        }

        public void SetCameraParameters(bool isAimed) => 
            _cameraAimer.SetCameraParameters(isAimed);

        public void SwitchToWinCamera() => 
            _cameraAimer.SwitchToWinCamera();
    }
}