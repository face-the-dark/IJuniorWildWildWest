using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyComponents
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private ShootPointsCalculator _shootPointsCalculator;
        [SerializeField] private float _distanceEpsilon = 0.01f;
        
        private NavMeshAgent  _navMeshAgent;
        
        private Coroutine _arrivalCoroutine;
        private float _lastVelocity;

        public event Action Moving;
        public event Action Arrived;
        public event Action<float> VelocityChanged;
        
        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void OnEnable()
        {
            Vector3 nearShootPosition = _shootPointsCalculator.CalculateNearShootPosition();
            
            MoveTo(nearShootPosition);
        }

        private void Update()
        {
            if (Mathf.Approximately(_lastVelocity, _navMeshAgent.velocity.magnitude) == false) 
            {
                _lastVelocity = _navMeshAgent.velocity.magnitude;
                
                VelocityChanged?.Invoke(_navMeshAgent.velocity.magnitude);
            }
        }

        public void MoveTo(Vector3 destination)
        {
            Moving?.Invoke();
            
            _navMeshAgent.destination = destination;
            
            StopArrivalCoroutine();
            _arrivalCoroutine = StartCoroutine(ConfirmArrival());
        }
        
        private void StopArrivalCoroutine()
        {
            if (_arrivalCoroutine != null)
            {
                StopCoroutine(_arrivalCoroutine);
                _arrivalCoroutine = null;
            }
        }

        private IEnumerator ConfirmArrival()
        {
            while (_navMeshAgent.pathPending 
                   || _navMeshAgent.hasPath
                   || _navMeshAgent.remainingDistance > _navMeshAgent.stoppingDistance 
                   || _navMeshAgent.velocity.sqrMagnitude > _distanceEpsilon)
                yield return null;
            
            Arrived?.Invoke();
        }
    }
}