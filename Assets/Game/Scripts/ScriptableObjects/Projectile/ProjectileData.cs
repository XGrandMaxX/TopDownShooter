using Game.Scripts.Objects.Projectiles;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
    [CreateAssetMenu(menuName = "New Projectile")]
    public sealed class ProjectileData : ScriptableObject
    {
        [SerializeField] internal PlayerProjectile ProjectilePrefab;

        [SerializeField] internal byte Damage;
        [SerializeField] internal byte Speed;
    }
}
