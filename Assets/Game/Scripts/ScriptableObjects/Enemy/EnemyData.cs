using UnityEngine;

namespace Game.Scripts.ScriptableObjects.Enemy
{
    [CreateAssetMenu(menuName = "New Enemy")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] internal byte Health;
        
        [SerializeField] internal byte MoveSpeed;
        [SerializeField] internal byte Damage;

        [SerializeField] internal int PointsOnDeath;
    }
}
