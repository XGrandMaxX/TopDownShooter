using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Game.Scripts.Enemies
{
    public sealed class EnemyListener
    {
        public event Action<int> OnEnemyDied;
        
        private List<EnemyData> _enemies = new(2);
        private EnemySpawner _enemySpawner;

        public EnemyListener() => _enemySpawner = Object.FindObjectOfType<EnemySpawner>();

        public void Subscribe(EnemyData enemy)
        {
            if(_enemies.Contains(enemy))
                return;
            
            _enemies.Add(enemy);
            enemy.OnDied += Unsubscribe;
        }


        private void Unsubscribe(EnemyData enemy)
        {
            OnEnemyDied?.Invoke(enemy.PointsOnDeath);
            
            _enemies.Remove(enemy);
            
            enemy.gameObject.SetActive(false);
            
            _enemySpawner._enemyPool.Return(enemy);
            _enemySpawner.currentEnemyInPool -= 1;
        }
    }
}
