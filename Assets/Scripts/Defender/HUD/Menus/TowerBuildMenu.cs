using System;
using Defender.HUD.Commands;
using Defender.Towers;
using Models;
using UnityEngine;
using Zenject;

namespace Defender.HUD.Menus
{
    [Serializable]
    public class TowerBuildMenu : GUIMenuBase
    {
        [SerializeField] private BuildTowerButton[] _buildTowerButtons;
        [SerializeField] private TowerBuilder _towerBuilder;

        private Wallet _wallet;

        [Inject]
        private void Construct(Wallet wallet)
        {
            _wallet = wallet;
        }

        public override void Init()
        {
            foreach (var button in _buildTowerButtons)
            {
                var buildTowerCommand = new BuildTowerCommand(this, _towerBuilder, button, _wallet);
                AssociateButton(button, buildTowerCommand);
            }
        }
    }
}