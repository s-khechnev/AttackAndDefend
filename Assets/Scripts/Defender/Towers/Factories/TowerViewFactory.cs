using System;
using UnityEngine;
using Zenject;

namespace Defender.Towers.Factories
{
    public class TowerViewFactory : ITowerViewFactory
    {
        public event Action<Tower> TowerTapped;

        private IInstantiator _instantiator;

        [Inject]
        private void Construct(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public TowerView Create(TowerView prefab, Vector3 position)
        {
            var towerView =
                _instantiator.InstantiatePrefabForComponent<TowerView>(prefab, position, Quaternion.identity, null);
            towerView.TowerTapped += OnTowerTapped;

            var tower = towerView.Tower;
            tower.enabled = false;
            return towerView;
        }

        private void OnTowerTapped(Tower tower)
        {
            TowerTapped?.Invoke(tower);
        }

        public void Destroy(TowerView gameObjectToDestroy)
        {
            UnityEngine.Object.Destroy(gameObjectToDestroy.gameObject);
        }
    }
}