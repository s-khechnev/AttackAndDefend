using Defender.Towers;
using UnityEngine;
using Zenject;

namespace ZenjectBinds
{
    public class TowerFactoryInstaller : MonoInstaller
    {
        [SerializeField] private TowerFactory _towerFactory;

        public override void InstallBindings()
        {
            Container.Bind<TowerFactory>().FromInstance(_towerFactory).AsSingle();
            Container.QueueForInject(_towerFactory);
        }
    }
}