using Defender.HUD;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Defender.Towers
{
    [CreateAssetMenu(fileName = "TowerFactory", menuName = "ScriptableObjects/TowerFactory")]
    public class TowerFactory : ScriptableObject
    {
        [Inject] private IInstantiator _instantiator;
        [Inject] private DefenderHUD _defenderHUD;

        public TowerGhost GetTowerGhost(Tower towerToGhost)
        {
            var tower = _instantiator.InstantiatePrefab(towerToGhost).GetComponent<Tower>();
            var towerGhost = tower.AddComponent<TowerGhost>();

            tower.TowerTapped += _defenderHUD.OnTowerTapped;
            
            towerGhost.Tower = tower;
            towerGhost.Tower.enabled = false;
            return towerGhost;
        }

        public void Reclaim(Tower tower)
        {
            Destroy(tower.gameObject);
        }

        public void Reclaim(TowerGhost towerGhost)
        {
            towerGhost.Tower.enabled = true;
            Destroy(towerGhost);
        }
    }
}