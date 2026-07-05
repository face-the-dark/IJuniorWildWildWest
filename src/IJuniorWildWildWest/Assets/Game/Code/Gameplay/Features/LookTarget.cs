using Game.Gameplay.Cameras.Provider;
using UnityEngine;
using VContainer;

namespace Game.Gameplay.Features
{
    public class LookTarget : MonoBehaviour
    {
        [SerializeField] private float _maxDistance = 100f;
    
        private ICameraProvider _cameraProvider;

        [Inject]
        public void Construct(ICameraProvider cameraProvider) => 
            _cameraProvider = cameraProvider;

        private void LateUpdate() => 
            transform.position = _cameraProvider.MainCamera.transform.position 
                                 + _cameraProvider.MainCamera.transform.forward * _maxDistance;
    }
}