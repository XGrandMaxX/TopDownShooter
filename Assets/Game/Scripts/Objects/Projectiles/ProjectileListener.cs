using System.Collections.Generic;
using Game.Scripts.Player.Weapons;
using UnityEngine;

namespace Game.Scripts.Objects.Projectiles
{
    public sealed class ProjectileListener
    {
        private List<PlayerProjectile> _projectiles = new(2);
        private Weapon _weapon;

        public ProjectileListener() => _weapon = Object.FindObjectOfType<Weapon>();

        public void Subscribe(PlayerProjectile projectile)
        {
            if(_projectiles.Contains(projectile))
                return;
            
            _projectiles.Add(projectile);
            
            projectile.OnDestroy += Unsubscribe;
        }

        private void Unsubscribe(PlayerProjectile projectile)
        {
            _projectiles.Remove(projectile);
            
            projectile.gameObject.SetActive(false);
            _weapon._projectilePool.Return(projectile); 
        }
    }
}
