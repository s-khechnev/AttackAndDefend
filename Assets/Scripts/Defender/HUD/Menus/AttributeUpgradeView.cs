using Defender.HUD.Bars;
using Defender.Towers.Base;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Defender.HUD.Menus
{
    public class AttributeUpgradeView : MonoBehaviour
    {
        [SerializeField] private AttributeBar _attributeBar;
        [SerializeField] private Button _attributeUpgradeButton;
        [SerializeField] private TMP_Text _attributeText;

        private Attribute _attribute;
        private Wallet _wallet;

        [Inject]
        private void Construct(Wallet wallet)
        {
            _wallet = wallet;
        }

        public void Init(Attribute attribute)
        {
            _attribute = attribute;

            _attributeBar.Init(attribute);
            _attributeText.text = attribute.Description;
            _attributeUpgradeButton.onClick.RemoveListener(OnUpgradeButtonClick);
            _attributeUpgradeButton.onClick.AddListener(OnUpgradeButtonClick);
        }

        private void OnUpgradeButtonClick()
        {
            if (!_wallet.IsEnoughMoney(_attribute.CostUpgrade) || !_attribute.CanUpgrade) return;

            _wallet.Purchase(_attribute.CostUpgrade);
            _attribute.Upgrade();
        }

        private void OnDisable()
        {
            _attributeUpgradeButton.onClick.RemoveListener(OnUpgradeButtonClick);
        }
    }
}