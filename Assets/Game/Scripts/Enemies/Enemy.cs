using System;
using Game.Scripts.ScriptableObjects.Enemy;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy : MonoBehaviour, IEnemy
    {
        public event Action<IEnemy> OnDied;
            
        private EnemyData _enemyData;
        private Rigidbody2D _rigidbody2D;

        [Inject]
        private void Construct(EnemyData enemyData)
        {
            _enemyData = enemyData;

            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        
        public void Attack()
        {
            throw new System.NotImplementedException();
        }

        public void TakeDamage(byte amount)
        {
            _enemyData.Health -= amount;

            if (_enemyData.Health <= 0)
                Die();
        }

        private void Die() => OnDied?.Invoke(this);
    }
}
