using UnityEngine;

namespace Game.Gameplay.Features
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private float _sphereRadius = 0.5f;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, _sphereRadius);
        }
    }
}