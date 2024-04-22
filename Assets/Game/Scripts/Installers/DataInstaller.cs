using Game.Scripts.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public sealed class DataInstaller : MonoInstaller
    {
        [SerializeField] private ProjectileData _projectileData;
        public override void InstallBindings() => Bind();

        private void Bind() => Container.BindInstance(_projectileData).AsTransient();
    }
}
