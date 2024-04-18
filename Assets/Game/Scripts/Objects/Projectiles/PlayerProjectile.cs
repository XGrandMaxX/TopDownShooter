using Game.Scripts.Enemies;
using Game.Scripts.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Objects.Projectiles
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerProjectile : MonoBehaviour
    {
        private ProjectileData _projectileData;
        private Rigidbody2D _rigidbody2D;
        
        [Inject]
        private void Construct(ProjectileData projectileData)
        {
            _projectileData = projectileData;

            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        //WIP direction issue
        public void Launch() 
            => _rigidbody2D.velocity = Vector2.up * _projectileData.Speed;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out IEnemy enemy))
                enemy.TakeDamage(_projectileData.Damage);
        }
    }
}
