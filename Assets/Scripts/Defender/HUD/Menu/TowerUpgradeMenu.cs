using System;
using System.Collections.Generic;
using Data.Towers;
using Defender.HUD.Command;
using Defender.Towers;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Defender.HUD.Menu
{
    [Serializable]
    public class TowerUpgradeMenu : GUIMenuBase
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private TMP_Text _towerName;
        [SerializeField] private Button _upgradeDamageButton;
        [SerializeField] private Button _upgradeRangeButton;
        [SerializeField] private Button _upgradeCooldownButton;
        [SerializeField] private Button _closeButton;

        [Inject] private Wallet _wallet;
        [Inject] private TowerFactory _towerFactory;

        private List<TowerUpgradeCommand> _towerUpgradeCommands;
        private TowerData _towerData;

        public override void Init()
        {
            Instance = _panel;

            var upgradeDamageCommand = new UpgradeDamageCommand(this, _wallet);
            var upgradeRangeCommand = new UpgradeRangeCommand(this, _wallet);
            var upgradeCooldownCommand = new UpgradeCooldownCommand(this, _wallet);

            _towerUpgradeCommands = new();
            _towerUpgradeCommands.Add(upgradeDamageCommand);
            _towerUpgradeCommands.Add(upgradeRangeCommand);
            _towerUpgradeCommands.Add(upgradeCooldownCommand);

            AssociateButton(_upgradeDamageButton, upgradeDamageCommand);
            AssociateButton(_upgradeRangeButton, upgradeRangeCommand);
            AssociateButton(_upgradeCooldownButton, upgradeCooldownCommand);
            AssociateButton(_closeButton, new CloseUpgradeCommand(this));

            _towerFactory.TowerTapped += OnTowerTapped;
            
            Hide();
        }

        private void OnTowerTapped(TowerData towerData)
        {
            Show();
            SetTowerData(towerData);
        }

        private void SetTowerData(TowerData towerData)
        {
            if (_towerData == towerData)
                return;

            _towerData = towerData;
            _towerUpgradeCommands.ForEach(command => command.SetTowerData(towerData));
        }
    }
}