using UnityEngine;

namespace Game.Scripts.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Enemy : EnemyData
    {
        private Rigidbody2D _rigidbody2D;
        
        
        private void Awake() => _rigidbody2D = GetComponent<Rigidbody2D>();

        protected internal override void Attack()
        {
            throw new System.NotImplementedException();
        }

        protected internal override void TakeDamage(byte amount)
        {
            Health -= amount;

            if (Health <= 0)
                Die();
        }
    }
}
