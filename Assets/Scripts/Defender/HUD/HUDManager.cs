using System;
using Data.Towers;
using TMPro;
using UnityEngine;

namespace Defender.HUD
{
    public class HUDManager : MonoBehaviour
    {
        public event Action<TowerData> BuildTowerTapped;

        [SerializeField] private TMP_Text _moneyText;

        private BuildTowerButton[] _buildTowerButtons;

        private void Awake()
        {
            _buildTowerButtons = FindObjectsOfType<BuildTowerButton>();

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            foreach (var button in _buildTowerButtons)
            {
                button.BuildTowerTapped += OnBuildTowerTapped;
            }

            GameManager.Instance.MoneyChanged += OnMoneyChanged;
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