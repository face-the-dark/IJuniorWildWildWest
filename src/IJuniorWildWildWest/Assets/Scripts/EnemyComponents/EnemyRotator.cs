using System.Collections;
using UnityEngine;

namespace EnemyComponents
{
    [RequireComponent(typeof(EnemyMover))]
    public class EnemyRotator : MonoBehaviour
    {
        [SerializeField] private Transform _player;

        private EnemyMover _mover;

        private Coroutine _lookCoroutine;

        private void Awake() => 
            _mover = GetComponent<EnemyMover>();

        private void OnEnable() => 
            _mover.Moving += OnMoving;

        private void OnDisable() => 
            _mover.Moving -= OnMoving;

        private void OnMoving()
        {
            StopLookCoroutine();
            
            _mover.Arrived += LookAtPlayer;
        }

        private void LookAtPlayer()
        {
            StopLookCoroutine();
            _lookCoroutine = StartCoroutine(LookAt());
            
            _mover.Arrived -= LookAtPlayer;
        }

        private void StopLookCoroutine()
        {
            if (_lookCoroutine != null)
            {
                StopCoroutine(_lookCoroutine);
                _lookCoroutine = null;
            }
        }

        private IEnumerator LookAt()
        {
            while (enabled)
            {
                transform.LookAt(_player);

                yield return null;
            }
        }
    }
}