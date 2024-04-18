using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Enemies
{
    //TODO: install this entity and inject to needed classes
    public class EnemyListener
    {
        private List<IEnemy> _enemies;

        public void Subscribe(IEnemy enemy)
        {
            if(_enemies.Contains(enemy))
                return;
            
            _enemies.Add(enemy);
            enemy.OnDied += Unsubscribe;
        }

        private void Unsubscribe(IEnemy enemy)
        {
            _enemies.Remove(enemy);
            
            Object.Destroy(enemy.gameObject);
        }
    }
}
