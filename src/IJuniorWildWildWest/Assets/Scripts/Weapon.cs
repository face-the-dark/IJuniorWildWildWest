using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private PlayerInputReader _playerInputReader;

    private void OnEnable()
    {
        _playerInputReader.Shoot += Shoot;
    }

    private void OnDisable()
    {
        _playerInputReader.Shoot -= Shoot;
    }

    private void Shoot()
    {
        Vector3 center = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        Ray ray = _mainCamera.ScreenPointToRay(center);

        Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity, _enemyLayer);

        if (hit.collider && hit.collider.TryGetComponent(out Heath health))
        {
            health.TakeDamage(_damage);
        }
    }
}