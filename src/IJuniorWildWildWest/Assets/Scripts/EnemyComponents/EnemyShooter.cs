using UnityEngine;

namespace EnemyComponents
{
    public class EnemyShooter : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private Weapon _weapon;
        [SerializeField] private Reload _reload; 

        public void Shoot()
        {
            transform.LookAt(_player);
            
            if (_reload.Expired)
            {
                Vector3 direction = _player.position - transform.position;

                _weapon.Fire(direction);
                _reload.Reset();
            }
        }
    }
}