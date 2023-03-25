﻿using System;
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

        [Inject] private Wallet _wallet;

        public override void Init()
        {
            foreach (var button in _buildTowerButtons)
            {
                var buildTowerCommand = new BuildTowerCommand(this, _towerBuilder, button.TowerToBuild, _wallet);
                AssociateButton(button, buildTowerCommand);
            }

            _towerBuilder.TowerPlaced += OnTowerPlaced;
        }

        private void OnTowerPlaced(TowerView placedTowerView)
        {
            _wallet.Purchase(placedTowerView.Tower.TowerData.Cost);
        }
    }
}