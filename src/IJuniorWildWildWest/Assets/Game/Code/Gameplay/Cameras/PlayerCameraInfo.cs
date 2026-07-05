using Cinemachine;
using UnityEngine;

namespace Game.Gameplay.Cameras
{
    public class PlayerCameraInfo : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _winVirtualCamera;
        [SerializeField] private Transform _cameraTarget;
        
        public CinemachineVirtualCamera WinVirtualCamera => _winVirtualCamera;
        public Transform CameraTarget => _cameraTarget;
    }
}