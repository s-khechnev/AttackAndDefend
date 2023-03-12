using System;
using Attackers.Waves;
using Defender.Towers;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Defender.HUD
{
    public class DefenderHUD : MonoBehaviour
    {
        public event Action<Tower> BuildTowerTapped;

        [SerializeField] private TowerUpgrader _towerUpgradePanel;
        [SerializeField] private TMP_Text _moneyText;
        [SerializeField] private Button _nextWaveButton;

        [Inject] private Spawner _spawner;
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
            _towerUpgradePanel.gameObject.SetActive(false);
            OnMoneyChanged(_wallet.Money);
        }

        private void SubscribeEvents()
        {
            foreach (var button in _buildTowerButtons)
            {
                button.BuildTowerTapped += OnBuildTowerTapped;
            }

            _spawner.WaveEnded += OnWaveEnded;
            _spawner.AllWavesEnded += OnAllWavesEnded;
            _nextWaveButton.onClick.AddListener(OnNextWaveTapped);

            _wallet.MoneyChanged += OnMoneyChanged;
        }

        private void OnAllWavesEnded()
        {
            Debug.Log("Waves are end");
        }

        private void OnWaveEnded()
        {
            _nextWaveButton.gameObject.SetActive(true);
        }

        private void OnNextWaveTapped()
        {
            _nextWaveButton.gameObject.SetActive(false);
            _spawner.StartNextWave();
        }

        private void OnMoneyChanged(int money)
        {
            _moneyText.text = money.ToString();
        }

        private void OnBuildTowerTapped(Tower tower)
        {
            BuildTowerTapped?.Invoke(tower);
        }

        public void OnTowerTapped(Tower tower)
        {
            _towerUpgradePanel.gameObject.SetActive(true);
            _towerUpgradePanel.Init(tower);
        }
    }
}