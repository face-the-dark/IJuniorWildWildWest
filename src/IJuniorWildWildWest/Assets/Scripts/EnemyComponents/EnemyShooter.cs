using System.Collections;
using UnityEngine;

namespace EnemyComponents
{
    public class EnemyShooter : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private Transform _player;
        [SerializeField] private float _reloadSpeed = 2f;
        
        [SerializeField] private EnemyVision _vision;
        [SerializeField] private EnemyMover _mover;
        
        private bool _isShooting;
        private WaitForSeconds _reloadWait;
        private Coroutine _shootCoroutine;

        private void Awake()
        {
            _reloadWait = new WaitForSeconds(_reloadSpeed);
        }

        private void OnEnable()
        {
            _vision.PlayerMissed += StopShoot;
            _mover.Arrived += StartShoot;
        }

        private void OnDisable()
        {
            _vision.PlayerMissed -= StopShoot;
            _mover.Arrived -= StartShoot;
        }

        private void StopShoot()
        {
            _isShooting = false;
            
            if (_shootCoroutine != null)
            {
                StopCoroutine(_shootCoroutine);
                _shootCoroutine = null;
            }
        }

        private void StartShoot()
        {
            StopShoot();
            
            _isShooting = true;
            _shootCoroutine = StartCoroutine(Shoot());
        }

        private IEnumerator Shoot()
        {
            while (_isShooting)
            {
                Vector3 direction = _player.position - transform.position;

                _weapon.Fire(direction);
                
                yield return _reloadWait;
            }
        }
    }
}