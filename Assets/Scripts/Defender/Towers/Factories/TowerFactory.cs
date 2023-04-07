using System;
using Defender.Towers.Base;
using Defender.Towers.Farming;
using UnityEngine;
using Zenject;

namespace Defender.Towers.Factories
{
    public class TowerFactory : ITowerFactory
    {
        public event Action<BaseTower> TowerTapped;

        private IInstantiator _instantiator;

        [Inject]
        private void Construct(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public BaseTower Create(BaseTower prefab, Vector3 position)
        {
            var tower = _instantiator.InstantiatePrefabForComponent<BaseTower>(prefab, position,
                prefab is FarmingTower ? Quaternion.Euler(0, 90, 0) : Quaternion.identity, null);

            tower.TowerTapped += OnTowerTapped;

            return tower;
        }

        private void OnTowerTapped(BaseTower tower)
        {
            TowerTapped?.Invoke(tower);
        }

        public void Destroy(BaseTower gameObjectToDestroy)
        {
            UnityEngine.Object.Destroy(gameObjectToDestroy.gameObject);
        }
    }
}