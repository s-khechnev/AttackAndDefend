using System.Collections;
using System.Collections.Generic;
using Defender.HUD.Commands;
using Defender.Towers;
using Defender.Towers.Attacking;
using Defender.Towers.Base;
using Defender.Towers.Factories;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Defender.HUD.Menus
{
    public class TowerInfoMenu : GUIMenuBase
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _closeMenuButton;
        [SerializeField] private Button _changeTargetSelectorButton;
        [SerializeField] private TMP_Text _targetSelectorView;
        [SerializeField] private Button _relocateTowerButton;
        [SerializeField] private TMP_Text _towerName;

        [SerializeField] private Transform _attributeContainer;

        [SerializeField] private TowerBuilder _towerBuilder;

        private BaseTower _currentTower;

        private RelocateTowerCommand _relocateTowerCommand;
        private ChangeTargetSelectorCommand _changeTargetSelectorCommand;

        private ITowerFactory _towerFactory;
        private Wallet _wallet;

        [Inject]
        private void Construct(ITowerFactory towerFactory, Wallet wallet)
        {
            _towerFactory = towerFactory;
            _wallet = wallet;
        }

        private void Awake()
        {
            Instance = _panel;

            InitButtons();

            _towerFactory.TowerTapped += OnTowerTapped;
        }

        private void Start()
        {
            Hide();
        }

        private void InitButtons()
        {
            _relocateTowerCommand = new RelocateTowerCommand(this, _towerBuilder, _wallet);
            AssociateButton(_relocateTowerButton, _relocateTowerCommand);

            _changeTargetSelectorCommand = new ChangeTargetSelectorCommand(this, _targetSelectorView);
            AssociateButton(_changeTargetSelectorButton, _changeTargetSelectorCommand);

            AssociateButton(_closeMenuButton, new CloseTowerInfoCommand(this));
        }

        private void OnTowerTapped(BaseTower tower)
        {
            if (_currentTower == tower)
                return;

            if (IsShown(Instance))
                _currentTower.TowerView.HideState();
            else
                Show();

            _currentTower = tower;

            SetTowerData(tower.BaseTowerData);
            _relocateTowerCommand.SetTower(tower);

            if (tower is AttackingTower attackingTower)
            {
                _changeTargetSelectorButton.gameObject.SetActive(true);
                _changeTargetSelectorCommand.SetTargetFinder(attackingTower.TargetFinder);
            }
            else
            {
                _changeTargetSelectorButton.gameObject.SetActive(false);
            }
            
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform) transform);
        }

        private void SetTowerData(BaseTowerData baseTowerData)
        {
            _towerName.text = baseTowerData.Name;
            ReinitAttributes(baseTowerData.Attributes);
        }

        private void ReinitAttributes(IReadOnlyList<Attribute> attributes)
        {
            for (var i = 0; i < _attributeContainer.childCount; i++)
            {
                _attributeContainer.GetChild(i).gameObject.SetActive(false);
            }

            for (var i = 0; i < attributes.Count; i++)
            {
                var attributeView = _attributeContainer.GetChild(i).GetComponent<AttributeUpgradeView>();
                attributeView.gameObject.SetActive(true);
                attributeView.Init(attributes[i]);
            }
        }

        public override void Hide()
        {
            base.Hide();

            if (_currentTower == null) return;

            _currentTower.TowerView.HideState();
            _currentTower = null;
        }
    }
}