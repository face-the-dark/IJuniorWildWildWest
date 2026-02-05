using System;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyComponents
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private float _distanceEpsilon = 0.01f;

        private NavMeshAgent _navMeshAgent;

        private float _lastVelocity;

        public event Action<float> VelocityChanged;

        private void Awake() => 
            _navMeshAgent = GetComponent<NavMeshAgent>();

        private void Update()
        {
            if (Mathf.Approximately(_lastVelocity, _navMeshAgent.velocity.magnitude) == false)
            {
                _lastVelocity = _navMeshAgent.velocity.magnitude;

                VelocityChanged?.Invoke(_navMeshAgent.velocity.magnitude);
            }
        }

        public void MoveTo(Vector3 destination) => 
            _navMeshAgent.destination = destination;

        public bool IsArrivedAtShootPoint() =>
            _navMeshAgent.pathPending == false
            && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance
            && _navMeshAgent.velocity.sqrMagnitude <= _distanceEpsilon;
    }
}