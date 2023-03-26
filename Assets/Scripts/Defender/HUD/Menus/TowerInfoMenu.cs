using System;
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
    public class TowerInfoMenu : GUIMenuBase
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private TowerUpgradeMenu _towerUpgradeMenu;
        [SerializeField] private Button _closeMenuButton;
        [SerializeField] private Button _relocateTowerButton;
        [SerializeField] private TMP_Text _towerName;

        [SerializeField] private TowerBuilder _towerBuilder;

        [Inject] private DiContainer _diContainer;

        [Inject] private TowerFactory _towerFactory;
        [Inject] private Wallet _wallet;
        
        private TowerData _currentTowerData;
        private TowerView _currentTowerView;

        private RelocateTowerCommand _relocateTowerCommand;
        
        public override void Init()
        {
            _diContainer.Inject(_towerUpgradeMenu);
            _towerUpgradeMenu.Init();

            Instance = _panel;
            
            AssociateButton(_closeMenuButton, new CloseTowerInfoCommand(this));

            _relocateTowerCommand = new RelocateTowerCommand(this, _towerBuilder, _wallet);
            AssociateButton(_relocateTowerButton, _relocateTowerCommand);

            _towerFactory.TowerTapped += OnTowerTapped;
            _towerBuilder.TowerRelocated += OnTowerRelocated;
            
            Hide();
        }

        private void OnTowerTapped(TowerView towerView)
        {
            var towerDataToUpgrade = towerView.Tower.TowerData;

            if (_currentTowerData == towerDataToUpgrade || DefenderGUIManager.GameState == DefenderGameState.Building)
                return;

            if (IsShown(Instance))
                _currentTowerView.HideState();

            Show();
            towerView.ShowState();
            
            _currentTowerData = towerDataToUpgrade;
            _currentTowerView = towerView;
            
            _relocateTowerCommand.SetTowerView(towerView);
            SetTowerData(towerDataToUpgrade);
        }

        private void OnTowerRelocated(TowerView towerView)
        {
            _wallet.Purchase(towerView.Tower.TowerData.CostToRelocate);
        }

        private void SetTowerData(TowerData towerData)
        {
            _towerName.text = towerData.Name;
            
            _towerUpgradeMenu.SetTowerData(towerData);
        }
        
        public override void Hide()
        {
            base.Hide();

            if (_currentTowerData == null || _currentTowerView == null) return;

            _currentTowerView.HideState();
            _currentTowerData = null;
        }

        public override bool IsShown(GameObject guiItem)
        {
            return guiItem.activeSelf;
        }
    }
}