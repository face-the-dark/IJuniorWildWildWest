using PlayerComponents;
using UnityEngine;

public class StormBoundary : MonoBehaviour
{
    [SerializeField] float pushForce = 25f;

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            Vector3 center = transform.position;
            Vector3 direction = (other.transform.forward - center).normalized;

            Rigidbody attachedRigidbody = other.attachedRigidbody;
            
            if (attachedRigidbody != null)
                attachedRigidbody.AddForce(direction * pushForce, ForceMode.Acceleration);
        }
    }
}