using Defender.Towers.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Defender.HUD
{
    public class BuildTowerButton : Button
    {
        [SerializeField] private BaseTower _tower;

        public BaseTower Tower => _tower;
    }
}