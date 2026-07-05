using UnityEngine;

namespace Game.Gameplay.Cameras
{
    [RequireComponent(typeof(CameraAimer))]
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;
        
        private CameraAimer _cameraAimer;

        public Transform ShootPoint => _shootPoint;
        
        public void Awake()
        {
            _cameraAimer = GetComponent<CameraAimer>();
        }

        public void SetCameraParameters(bool isAimed) => 
            _cameraAimer.SetCameraParameters(isAimed);

        public void SwitchToWinCamera() => 
            _cameraAimer.SwitchToWinCamera();
    }
}