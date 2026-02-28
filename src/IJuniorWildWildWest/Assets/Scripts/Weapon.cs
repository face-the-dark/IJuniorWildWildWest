using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float _damage = 10f;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private LayerMask _targetLayer;
    
    public event Action Firing;
    public event Action<Vector3> Hit;
    
    public void Construct(Transform shootPoint) => 
        _shootPoint = shootPoint;

    public void Fire(Vector3 direction)
    {
        Firing?.Invoke();
        
        Ray ray = new Ray(_shootPoint.position, direction);
        
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider && hit.collider.TryGetComponent(out Health health))
            {
                health.TakeDamage(_damage);
                
                Hit?.Invoke(hit.point);
            }
        }
    }
}