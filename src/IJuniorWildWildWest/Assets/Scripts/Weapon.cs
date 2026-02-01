using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float _damage = 10f;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private LayerMask _targetLayer;
    
    public void Fire(Vector3 direction)
    {
        Ray ray;

        if (_shootPoint == null)
            ray = new Ray(direction, Vector3.forward);   
        else
            ray = new Ray(_shootPoint.position, direction);

        if (Physics.Raycast(ray, out RaycastHit hit))
            if (hit.collider && hit.collider.TryGetComponent(out Health health))
            {
                health.TakeDamage(_damage);
                
                Debug.Log($"Hit {health.gameObject.name}");
            }
    }
}