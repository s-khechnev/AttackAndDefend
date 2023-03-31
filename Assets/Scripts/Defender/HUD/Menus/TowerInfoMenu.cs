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
        [SerializeField] private Button _closeMenuButton;
        [SerializeField] private Button _changeTargetSelectorButton;
        [SerializeField] private TMP_Text _targetSelectorDescription;
        [SerializeField] private Button _relocateTowerButton;
        [SerializeField] private TMP_Text _towerName;

        [SerializeField] private TowerBuilder _towerBuilder;

        [Inject] private TowerFactory _towerFactory;
        [Inject] private Wallet _wallet;
        
        private TowerData _currentTowerData;
        private TowerView _currentTowerView;

        private RelocateTowerCommand _relocateTowerCommand;
        private ChangeTargetSelectorCommand _changeTargetSelectorCommand;

        public override void Init()
        {
            Instance = _panel;
            
            _relocateTowerCommand = new RelocateTowerCommand(this, _towerBuilder, _wallet);
            AssociateButton(_relocateTowerButton, _relocateTowerCommand);
            
            _changeTargetSelectorCommand = new ChangeTargetSelectorCommand(this, _targetSelectorDescription);
            AssociateButton(_changeTargetSelectorButton, _changeTargetSelectorCommand);
            
            AssociateButton(_closeMenuButton, new CloseTowerInfoCommand(this));
            
            _towerFactory.TowerTapped += OnTowerTapped;

            Hide();
        }

        private void OnTowerTapped(Tower tower)
        {
            if (_currentTowerData == tower.TowerData)
                return;

            if (IsShown(Instance))
                _currentTowerView.HideState();

            _currentTowerData = tower.TowerData;
            _currentTowerView = tower.TowerView;

            SetTowerData(tower.TowerData);
            _relocateTowerCommand.SetTowerView(tower.TowerView);
            _changeTargetSelectorCommand.SetTargetFinder(tower.TargetFinder);

            Show();
        }

        private void SetTowerData(TowerData towerData)
        {
            _towerName.text = towerData.Name;
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