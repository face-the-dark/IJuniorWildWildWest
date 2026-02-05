using PlayerComponents;
using UnityEngine;

namespace CameraComponents
{
    [RequireComponent(typeof(CameraAimer))]
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;
        
        private CameraAimer _cameraAimer;

        public Transform ShootPoint => _shootPoint;
        
        public void Construct(Player player, Transform cameraTarget)
        {
            _cameraAimer = GetComponent<CameraAimer>();
            _cameraAimer.Construct(player, cameraTarget);
        }
    }
}