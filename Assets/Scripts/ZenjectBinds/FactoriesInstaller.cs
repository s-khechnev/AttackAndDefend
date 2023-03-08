using Attackers;
using Defender.Towers;
using UnityEngine;
using Zenject;

namespace ZenjectBinds
{
    public class FactoriesInstaller : MonoInstaller
    {
        [SerializeField] private TowerFactory _towerFactory;
        [SerializeField] private AttackerFactory _attackerFactory;
        [SerializeField] private WarFactory _warFactory;

        public override void InstallBindings()
        {
            InstallAttackerFactory();
            InstallTowerFactory();
            InstallWarFactory();
        }

        private void InstallWarFactory()
        {
            Container.Bind<WarFactory>().FromNewScriptableObject(_warFactory).AsSingle();
            Container.QueueForInject(_warFactory);
        }

        private void InstallAttackerFactory()
        {
            Container.Bind<AttackerFactory>().FromNewScriptableObject(_attackerFactory).AsSingle();
            Container.QueueForInject(_attackerFactory);
        }

        private void InstallTowerFactory()
        {
            Container.Bind<TowerFactory>().FromNewScriptableObject(_towerFactory).AsSingle();
            Container.QueueForInject(_towerFactory);
        }
    }
}