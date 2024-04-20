using System;
using Game.Scripts.Enemies;
using UnityEngine;

namespace Game.Scripts.Player
{
    public sealed class PlayerCollision : MonoBehaviour
    {
        public static event Action<byte> OnTakenDamage;
        public static event Action OnDied;

        internal const byte MaxHealth = 4;

        internal bool _invincible;

        private byte _health = 4;
        
        private PlayerInvincible _playerInvincible;

        private void Awake() => _playerInvincible = GetComponent<PlayerInvincible>();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(!other.gameObject.TryGetComponent(out EnemyData enemy))
                return;
            
            TakeDamage(enemy.Damage);
        }

        private void TakeDamage(byte amount)
        {
            if(_invincible)
                return;
            
            _health -= amount;

            OnTakenDamage?.Invoke(_health);

            _playerInvincible.Activate();

            if (_health > 0)
                return;

            gameObject.SetActive(false);
            
            OnDied?.Invoke();
        }
    }
}
