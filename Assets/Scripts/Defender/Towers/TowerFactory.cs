using System;
using UnityEngine;
using Zenject;

namespace Defender.Towers
{
    [CreateAssetMenu(fileName = "TowerFactory", menuName = "ScriptableObjects/TowerFactory")]
    public class TowerFactory : ScriptableObject
    {
        public event Action<Tower> TowerTapped;

        [Inject] private IInstantiator _instantiator;

        public TowerView GetTowerView(Tower towerPrefab)
        {
            var tower = _instantiator.InstantiatePrefabForComponent<Tower>(towerPrefab);
            tower.enabled = false;

            var towerView = tower.GetComponent<TowerView>();
            towerView.TowerTapped += OnTowerTapped;

            return towerView;
        }

        private void OnTowerTapped(Tower tower)
        {
            TowerTapped?.Invoke(tower);
        }

        public void Reclaim(Tower tower)
        {
            Destroy(tower.gameObject);
        }
    }
}