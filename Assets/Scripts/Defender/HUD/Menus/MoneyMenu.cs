using Models;
using TMPro;
using UnityEngine;
using Zenject;

namespace Defender.HUD.Menus
{
    public class MoneyMenu : GUIMenuBase
    {
        [SerializeField] private TMP_Text _moneyText;

        /// <summary>
        /// The wallet which balance that is currently displayed
        /// </summary>
        private Wallet _wallet;

        [Inject]
        private void Construct(Wallet wallet)
        {
            _wallet = wallet;
        }

        private void Awake()
        {
            _wallet.MoneyChanged += OnMoneyChanged;

            OnMoneyChanged(_wallet.Money);
        }

        private void OnMoneyChanged(int amount)
        {
            _moneyText.text = amount.ToString();
        }
    }
}