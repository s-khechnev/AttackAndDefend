using System;
using System.Collections.Generic;
using Defender.HUD.Bars;
using Defender.HUD.Commands;
using Defender.Towers;
using Models;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Defender.HUD.Menus
{
    [Serializable]
    public class TowerUpgradeMenu : GUIMenuBase
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _upgradeDamageButton;
        [SerializeField] private Button _upgradeRangeButton;
        [SerializeField] private Button _upgradeCooldownButton;

        [SerializeField] private TowerAttributeBar _damageUpgradeBar;
        [SerializeField] private TowerAttributeBar _rangeUpgradeBar;
        [SerializeField] private TowerAttributeBar _cooldownUpgradeBar;

        [Inject] private Wallet _wallet;

        private List<TowerUpgradeCommand> _towerUpgradeCommands;

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
        }

        public void SetTowerData(TowerData towerData)
        {
            _towerUpgradeCommands.ForEach(command => command.SetTowerData(towerData));

            _damageUpgradeBar.Init(towerData.Damage);
            _rangeUpgradeBar.Init(towerData.Range);
            _cooldownUpgradeBar.Init(towerData.Cooldown);
        }
    }
}