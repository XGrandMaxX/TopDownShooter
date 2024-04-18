using System;
using UnityEngine;

namespace Game.Scripts.Enemies
{
    public interface IEnemy
    {
        GameObject gameObject => default;
        
        public event Action<IEnemy> OnDied;
        
        void Attack();
        void TakeDamage(byte amount);
    }
}
