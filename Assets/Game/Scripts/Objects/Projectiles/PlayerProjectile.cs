using System;
using Game.Scripts.Enemies;
using Game.Scripts.ScriptableObjects;
using UnityEngine;

namespace Game.Scripts.Objects.Projectiles
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerProjectile : MonoBehaviour, IProjectile
    {
        public event Action<PlayerProjectile> OnDestroy;
        
        [field: SerializeField] public byte LifeTime { get; private set;  }
        
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private ProjectileData _projectileData;
        public void Launch(Quaternion weaponRotation)
        {
            Invoke(nameof(DestroyProjectile), LifeTime);

            Vector2 direction = weaponRotation * Vector2.right;
            _rigidbody2D.velocity = direction.normalized * _projectileData.Speed;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyData enemy))
                enemy.TakeDamage(_projectileData.Damage);
            
            DestroyProjectile();
        }

        public void DestroyProjectile() => OnDestroy?.Invoke(this);
    }
}
