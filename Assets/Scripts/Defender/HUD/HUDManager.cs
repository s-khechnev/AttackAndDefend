using System;
using Attacker.Waves;
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
        public event Action NextWaveTapped;

        [SerializeField] private TMP_Text _moneyText;
        [SerializeField] private Button _nextWaveButton;
        [SerializeField] private Spawner _spawner;

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
            NextWaveTapped?.Invoke();
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