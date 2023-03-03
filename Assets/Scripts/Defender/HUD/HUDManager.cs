using System;
using Data.Towers;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Defender.HUD
{
    public class HUDManager : MonoBehaviour
    {
        public event Action<TowerData> BuildTowerTapped;

        [SerializeField] private TMP_Text _moneyText;
        [SerializeField] private Button _nextWaveButton;

        [Inject] private Wallet _wallet;
        private BuildTowerButton[] _buildTowerButtons;

        private void Awake()
        {
            _buildTowerButtons = FindObjectsOfType<BuildTowerButton>();

            SubscribeEvents();
        }

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            OnMoneyChanged(_wallet.Money);
        }

        private void SubscribeEvents()
        {
            foreach (var button in _buildTowerButtons)
            {
                button.BuildTowerTapped += OnBuildTowerTapped;
            }
            
            _nextWaveButton.onClick.AddListener(OnNextWaveTapped);

            _wallet.MoneyChanged += OnMoneyChanged;
        }

        private void OnNextWaveTapped()
        {
            
        }

        private void OnMoneyChanged(int money)
        {
            _moneyText.text = money.ToString();
        }

        private void OnBuildTowerTapped(TowerData towerData)
        {
            BuildTowerTapped?.Invoke(towerData);
        }
    }
}