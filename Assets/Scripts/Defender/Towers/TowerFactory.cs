using Data.Towers;
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
            var tower = Instantiate(towerData.TowerPrefab);
            tower.enabled = false;
            var towerGhost = tower.gameObject.AddComponent<TowerGhost>();
            towerGhost.TowerData = towerData;
            return towerGhost;
        }

        public Tower Get(TowerData towerData)
        {
            switch (towerData.Type)
            {
                case TowerType.Common:
                    return _instantiator.InstantiatePrefab(_defaultTowerData.TowerPrefab).GetComponent<Tower>();
            }

            return null;
        }

        public TowerData GetTowerData(TowerType towerType)
        {
            switch (towerType)
            {
                case TowerType.Common:
                    return _defaultTowerData;
            }

            return null;
        }

        public void Reclaim(Tower tower)
        {
            Destroy(tower.gameObject);
        }
        
        public void Reclaim(TowerGhost towerGhost)
        {
            towerGhost.GetComponent<Tower>().enabled = true;
            Destroy(towerGhost.gameObject);
        }
    }
}