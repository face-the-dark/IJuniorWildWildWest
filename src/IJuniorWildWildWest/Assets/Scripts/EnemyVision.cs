using System;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _viewFieldAngle = 90f;
    [SerializeField] private float _viewFieldDistance = 10f;
    [SerializeField] private LayerMask _obstacleLayer;
    
    public event Action<Vector3> PlayerFound;

    private void Update()
    {
        FindPlayer();
    }

    private void FindPlayer()
    {
        Vector3 directionToPlayer = (_player.transform.position - transform.position).normalized;

        float distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);

        if (distanceToPlayer <= _viewFieldDistance)
        {
            float angle = Vector3.Angle(transform.forward, directionToPlayer);

            if (angle < _viewFieldAngle / 2)
            {
                bool raycast = Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, _viewFieldDistance,
                    ~_obstacleLayer);
                
                if (raycast && hit.collider && hit.collider.TryGetComponent(out Player _player))
                {
                    PlayerFound?.Invoke(_player.transform.position);
                }
            }
        }
    }
}