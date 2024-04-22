using System;
using UnityEngine;

namespace Game.Scripts.Enemies
{
    public abstract class EnemyData : MonoBehaviour
    {
        protected internal event Action<EnemyData> OnDied;
        
        protected internal AudioSource _audioSource;
        
        [SerializeField] protected internal byte Health;
        
        [SerializeField] protected internal byte MoveSpeed;
        [SerializeField] protected internal byte Damage;

        [SerializeField] protected internal int PointsOnDeath;
        
        protected Transform _chaseTarget;
        protected Animator _animator;
        protected Vector2 _moveDirection;
        
        protected internal abstract void Initialize(byte health);
        protected internal abstract void TakeDamage(byte amount);
        protected internal abstract void ChaseThePlayer();
        protected void Die() => OnDied?.Invoke(this);
        
        protected Rigidbody2D _rigidbody2D;
    }
}
