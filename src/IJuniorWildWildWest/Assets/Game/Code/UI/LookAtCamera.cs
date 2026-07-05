using Game.Gameplay.Cameras.Provider;
using UnityEngine;
using VContainer;

namespace Game.UI
{
    public class LookAtCamera : MonoBehaviour
    {
        private Camera _mainCamera;

        [Inject]
        public void Construct(ICameraProvider cameraProvider)
        {
            _mainCamera = cameraProvider.MainCamera;
        }

        private void Update()
        {
            Quaternion rotation = _mainCamera.transform.rotation;
            transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
        }
    }
}