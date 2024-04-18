using Game.Scripts.ScriptableObjects;
using Game.Scripts.ScriptableObjects.Enemy;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class DataInstaller : MonoInstaller
    {
        [SerializeField] private ProjectileData _projectileData;
        [SerializeField] private EnemyData _enemyData;
        public override void InstallBindings() => Bind();

        private void Bind()
        {
            Container.BindInstance(_projectileData).AsTransient();
            Container.BindInstance(_enemyData).AsTransient();
        }
    }
}
