﻿using Attackers;
using Defender.Towers;
using UnityEngine;
using Zenject;

namespace ZenjectBinds
{
    public class FactoriesInstaller : MonoInstaller
    {
        [SerializeField] private TowerFactory _towerFactory;
        [SerializeField] private AttackerFactory _attackerFactory;

        public override void InstallBindings()
        {
            InstallAttackerFactory();
            InstallTowerFactory();
        }

        private void InstallAttackerFactory()
        {
            Container.Bind<AttackerFactory>().FromInstance(_attackerFactory).AsSingle();
            Container.QueueForInject(_attackerFactory);
        }

        private void InstallTowerFactory()
        {
            Container.Bind<TowerFactory>().FromInstance(_towerFactory).AsSingle();
            Container.QueueForInject(_towerFactory);
        }
    }
}