using System;
using System.Collections.Generic;
using Defender.HUD.Bars;
using Defender.HUD.Commands;
using Defender.Towers;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Defender.HUD.Menus
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

        [SerializeField] private TowerAttributeBar _damageUpgradeBar;
        [SerializeField] private TowerAttributeBar _rangeUpgradeBar;
        [SerializeField] private TowerAttributeBar _cooldownUpgradeBar;

        [Inject] private Wallet _wallet;
        [Inject] private TowerFactory _towerFactory;

        private List<TowerUpgradeCommand> _towerUpgradeCommands;

        private TowerData _currentTowerData;
        private RangeViewer _currentRangeViewer;

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

        private void OnTowerTapped(TowerData towerDataToUpgrade, RangeViewer rangeViewer)
        {
            if (_currentTowerData == towerDataToUpgrade || DefenderGUIManager.GameState == DefenderGameState.Building)
                return;

            if (IsShown(Instance))
                _currentRangeViewer.Hide();

            _currentTowerData = towerDataToUpgrade;
            _currentRangeViewer = rangeViewer;

            Show();
            rangeViewer.Show();
            SetTowerData(towerDataToUpgrade);
        }

        private void SetTowerData(TowerData towerData)
        {
            _towerUpgradeCommands.ForEach(command => command.SetTowerData(towerData));

            _towerName.text = towerData.Name;

            _damageUpgradeBar.Init(towerData.Damage);
            _rangeUpgradeBar.Init(towerData.Range);
            _cooldownUpgradeBar.Init(towerData.Cooldown);
        }

        public override void Hide()
        {
            base.Hide();

            if (_currentTowerData == null || _currentRangeViewer == null) return;

            _currentRangeViewer.Hide();
            _currentRangeViewer.Hide();
            _currentTowerData = null;
        }

        public override bool IsShown(GameObject guiItem)
        {
            return guiItem.activeSelf;
        }
    }
}