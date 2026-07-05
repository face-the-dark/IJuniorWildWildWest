using Game.Gameplay.Cameras.Provider;
using UnityEngine;
using VContainer;

namespace Game.Gameplay.Features
{
    public class DirectionCalculator
    {
        private readonly ICameraProvider _cameraProvider;

        [Inject]
        public DirectionCalculator(ICameraProvider cameraProvider) => 
            _cameraProvider = cameraProvider;

        public Vector3 CalculateCameraViewDirection(Vector2 inputDirection)
        {
            Vector3 direction = new Vector3(inputDirection.x, 0f, inputDirection.y);

            Vector3 cameraForward = _cameraProvider.MainCamera.transform.forward;
            Vector3 cameraRight = _cameraProvider.MainCamera.transform.right;

            cameraForward.y = 0f;
            cameraRight.y = 0f;

            return cameraForward.normalized * direction.z + cameraRight.normalized * direction.x;
        }
    }
}