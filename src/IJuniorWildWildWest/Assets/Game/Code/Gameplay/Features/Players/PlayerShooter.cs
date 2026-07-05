using Game.Gameplay.Cameras.Provider;
using UnityEngine;
using VContainer;

namespace Game.Gameplay.Features.Players
{
    [RequireComponent(typeof(PlayerInputReader))]
    public class PlayerShooter : MonoBehaviour
    {
        private Weapon _weapon;
        private ICameraProvider _cameraProvider;

        private bool _isAimed;

        [Inject]
        public void Construct(ICameraProvider cameraProvider, PlayerDataProvider playerDataProvider)
        {
            _cameraProvider = cameraProvider;
            _weapon = playerDataProvider.Weapon;
        }

        public void SetAimed(bool isAimed) => 
            _isAimed = isAimed;

        public void Shoot()
        {
            if (_isAimed) 
                _weapon.Fire(_cameraProvider.MainCamera.transform.forward);
        }
    }
}