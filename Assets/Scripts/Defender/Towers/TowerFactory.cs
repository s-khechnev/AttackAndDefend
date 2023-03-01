﻿using Data.Towers;
using UnityEngine;
using Zenject;

namespace Defender.Towers
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TowerFactory")]
    public class TowerFactory : ScriptableObject
    {
        [SerializeField] private TowerData _defaultTowerData;

        [Inject] private IInstantiator _instantiator;

        public TowerGhost GetTowerGhost(TowerData towerData)
        {
            var obj = Instantiate(towerData.TowerPrefab);
            var towerGhost = obj.AddComponent<TowerGhost>();
            towerGhost.TowerData = towerData;
            return towerGhost;
        }

        public Tower Get(TowerData towerData)
        {
            switch (towerData.TowerType)
            {
                case TowerType.DefaultTower:
                    var pref = _instantiator.InstantiatePrefab(_defaultTowerData.TowerPrefab);
                    return _instantiator.InstantiateComponent<DefaultTower>(pref);
            }

            return null;
        }

        public TowerData GetTowerData(TowerType towerType)
        {
            switch (towerType)
            {
                case TowerType.DefaultTower:
                    return _defaultTowerData;
            }

            return null;
        }
    }
}