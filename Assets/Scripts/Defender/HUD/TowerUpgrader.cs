using System;
using Defender.Towers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Defender.HUD
{
    public class TowerUpgrader : MonoBehaviour
    {
        [SerializeField] private TMP_Text _towerName;
        [SerializeField] private Button _upgradeDamageButton;
        [SerializeField] private Button _upgradeRangeButton;
        [SerializeField] private Button _upgradeCooldownButton;
        [SerializeField] private Button _closeButton;
        
        private Tower _tower;
        
        public void Init(Tower towerToUpgrade)
        {
            _tower = towerToUpgrade;
            _towerName.text = _tower.GetInstanceID().ToString();
        }

        private void OnEnable()
        {
            _upgradeDamageButton.onClick.AddListener(OnUpgradeDamageButtonClick);
            _upgradeRangeButton.onClick.AddListener(OnUpgradeRangeButtonClick);
            _upgradeCooldownButton.onClick.AddListener(OnUpgradeCooldownButtonClick);
            _closeButton.onClick.AddListener(OnCloseButtonClick);
        }

        private void OnDisable()
        {
            _upgradeDamageButton.onClick.RemoveListener(OnUpgradeDamageButtonClick);
            _upgradeRangeButton.onClick.RemoveListener(OnUpgradeRangeButtonClick);
            _upgradeCooldownButton.onClick.RemoveListener(OnUpgradeCooldownButtonClick);
            _closeButton.onClick.RemoveListener(OnCloseButtonClick);
        }
        
        private void OnCloseButtonClick()
        {
            gameObject.SetActive(false);
        }

        private void OnUpgradeDamageButtonClick()
        {
            throw new NotImplementedException();
        }
        
        private void OnUpgradeRangeButtonClick()
        {
            throw new NotImplementedException();
        }

        private void OnUpgradeCooldownButtonClick()
        {
            throw new NotImplementedException();
        }
    }
}