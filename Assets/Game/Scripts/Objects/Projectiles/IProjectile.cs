using System;
using UnityEngine;

namespace Game.Scripts.Objects.Projectiles
{
    public interface IProjectile
    {
        GameObject gameObject { get; }
        
        event Action<PlayerProjectile> OnDestroy;
        byte LifeTime { get; }
        void Launch(Quaternion weaponRotation);
        void DestroyProjectile();
    }
}
