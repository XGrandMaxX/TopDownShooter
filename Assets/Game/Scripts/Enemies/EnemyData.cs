using System;
using UnityEngine;

namespace Game.Scripts.Enemies
{
    public abstract class EnemyData : MonoBehaviour
    {
        protected internal event Action<EnemyData> OnDied;
        
        [SerializeField] protected internal byte Health;
        
        [SerializeField] protected internal byte MoveSpeed;
        [SerializeField] protected internal byte Damage;

        [SerializeField] protected internal int PointsOnDeath;

        protected internal abstract void TakeDamage(byte amount);
        protected internal abstract void Attack();
        protected void Die() => OnDied?.Invoke(this);
    }
}
