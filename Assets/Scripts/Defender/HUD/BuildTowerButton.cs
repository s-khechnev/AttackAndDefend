using Defender.Towers;
using UnityEngine;
using UnityEngine.UI;

namespace Defender.HUD
{
    public class BuildTowerButton : Button
    {
        [SerializeField] private Tower _towerToBuild;
        public Tower TowerToBuild => _towerToBuild;
    }
}