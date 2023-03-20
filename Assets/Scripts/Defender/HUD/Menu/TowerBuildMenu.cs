using System;
using Defender.HUD.Command;
using Defender.Towers;
using UnityEngine;

namespace Defender.HUD.Menu
{
    [Serializable]
    public class TowerBuildMenu : GUIMenuBase
    {
        [SerializeField] private BuildTowerButton[] _buildTowerButtons;
        [SerializeField] private TowerBuilder _towerBuilder;

        public override void Init()
        {
            foreach (var button in _buildTowerButtons)
            {
                var buildTowerCommand = new BuildTowerCommand(this, _towerBuilder, button.TowerToBuild);
                AssociateButton(button, buildTowerCommand);
            }
        }
    }
}