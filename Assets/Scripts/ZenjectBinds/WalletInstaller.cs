using Models;
using Zenject;

namespace ZenjectBinds
{
    public class WalletInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Wallet>().FromNew().AsSingle();
        }
    }
}
