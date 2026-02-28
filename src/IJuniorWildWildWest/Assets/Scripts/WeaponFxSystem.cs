using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class WeaponFxSystem : MonoBehaviour
{
    [SerializeField] private Transform _gunSmokeFxSpawnPoint;
    [SerializeField] private ParticleSystem _gunSmokeFx;
    [SerializeField] private ParticleSystem _bloodSplatFx;

    private Weapon _weapon;

    private void Awake() => 
        _weapon = GetComponent<Weapon>();

    private void OnEnable()
    {
        _weapon.Firing += OnFiring;
        _weapon.Hit += OnHit;
    }

    private void OnDisable()
    {
        _weapon.Firing -= OnFiring;
        _weapon.Hit -= OnHit;
    }

    private void OnFiring() => 
        Instantiate(_gunSmokeFx, _gunSmokeFxSpawnPoint.position, Quaternion.identity);

    private void OnHit(Vector3 hitPoint) => 
        Instantiate(_bloodSplatFx, hitPoint, Quaternion.identity);
}