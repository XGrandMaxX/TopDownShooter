using Game.Scripts.Enemies;
using Game.Scripts.Objects.Projectiles;
using Zenject;

namespace Game.Scripts.Installers
{
    public sealed class ListenersInstaller : MonoInstaller
    {
        public override void InstallBindings() => Bind();

        private void Bind()
        {
            Container.Bind<ProjectileListener>().AsSingle().NonLazy();
            Container.Bind<EnemyListener>().AsSingle().NonLazy();
        }
    }
}
