using System;
using Defender.HUD.Bars;
using Defender.HUD.Commands;
using Defender.Towers;
using Defender.Towers.Factories;
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

        private UpgradeAttributeCommand _upgradeDamageCommand;
        private UpgradeAttributeCommand _upgradeRangeCommand;
        private UpgradeAttributeCommand _upgradeCooldownCommand;
        
        private ITowerViewFactory _towerFactory;
        private Wallet _wallet;
        
        [Inject]
        private void Construct(ITowerViewFactory towerViewFactory, Wallet wallet)
        {
            _towerFactory = towerViewFactory;
            _wallet = wallet;
        }

        public override void Init()
        {
            Instance = _panel;

            _upgradeDamageCommand = new UpgradeAttributeCommand(this, _wallet);
            _upgradeRangeCommand = new UpgradeAttributeCommand(this, _wallet);
            _upgradeCooldownCommand = new UpgradeAttributeCommand(this, _wallet);

            AssociateButton(_upgradeDamageButton, _upgradeDamageCommand);
            AssociateButton(_upgradeRangeButton, _upgradeRangeCommand);
            AssociateButton(_upgradeCooldownButton, _upgradeCooldownCommand);

            _towerFactory.TowerTapped += OnTowerTapped;
        }

        private void OnTowerTapped(Tower tower)
        {
            SetAttributes(tower.TowerData);   
        }

        private void SetAttributes(TowerData towerData)
        {
            _upgradeDamageCommand.SetAttribute(towerData.Damage);
            _upgradeRangeCommand.SetAttribute(towerData.Range);
            _upgradeCooldownCommand.SetAttribute(towerData.Cooldown);

            _damageUpgradeBar.Init(towerData.Damage);
            _rangeUpgradeBar.Init(towerData.Range);
            _cooldownUpgradeBar.Init(towerData.Cooldown);
        }
    }
}