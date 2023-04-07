using Attackers;
using Defender.Towers.Factories;
using Zenject;

namespace ZenjectBinds
{
    public class FactoriesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallAttackerFactory();
            InstallTowerFactory();
            InstallWarFactory();
        }

        private void InstallWarFactory()
        {
            Container.Bind<IWarFactory>().To<WarFactory>().FromNew().AsSingle();
        }

        private void InstallAttackerFactory()
        {
            Container.Bind<IAttackerFactory>().To<AttackerFactory>().FromNew().AsSingle();
        }

        private void InstallTowerFactory()
        {
            Container.Bind<ITowerFactory>().To<TowerFactory>().FromNew().AsSingle();
        }
    }
}