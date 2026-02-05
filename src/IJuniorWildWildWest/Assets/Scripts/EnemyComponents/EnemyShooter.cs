using UnityEngine;

namespace EnemyComponents
{
    public class EnemyShooter : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private Reload _reload;
        
        private Transform _player;

        public void Construct(Transform player) => 
            _player = player;

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