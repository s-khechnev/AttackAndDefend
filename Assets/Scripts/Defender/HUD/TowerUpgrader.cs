using Data.Towers;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Defender.HUD
{
    public class TowerUpgrader : MonoBehaviour
    {
        [SerializeField] private TMP_Text _towerName;
        [SerializeField] private Button _upgradeDamageButton;
        [SerializeField] private Button _upgradeRangeButton;
        [SerializeField] private Button _upgradeCooldownButton;
        [SerializeField] private Button _closeButton;

        [Inject] private Wallet _wallet;

        private TowerData _towerData;

        public void Init(TowerData towerToUpgrade)
        {
            _towerData = towerToUpgrade;

            _towerName.text = _towerData.Name;
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
            if (!_wallet.IsEnoughMoney(_towerData.Damage.CostUpgrade) || !_towerData.Damage.CanUpgrade)
                return;

            _wallet.Purchase(_towerData.Damage.CostUpgrade);
            _towerData.UpgradeDamage();
        }

        private void OnUpgradeRangeButtonClick()
        {
            if (!_wallet.IsEnoughMoney(_towerData.Range.CostUpgrade) || !_towerData.Range.CanUpgrade)
                return;

            _wallet.Purchase(_towerData.Range.CostUpgrade);
            _towerData.UpgradeRange();
        }

        private void OnUpgradeCooldownButtonClick()
        {
            if (!_wallet.IsEnoughMoney(_towerData.Cooldown.CostUpgrade) || !_towerData.Cooldown.CanUpgrade)
                return;

            _wallet.Purchase(_towerData.Cooldown.CostUpgrade);
            _towerData.UpgradeCooldown();
        }
    }
}