using System;
using Models;
using TMPro;
using UnityEngine;
using Zenject;

namespace Defender.HUD.Menu
{
    [Serializable]
    public class MoneyMenu : GUIMenuBase
    {
        [SerializeField] private TMP_Text _moneyText;

        [Inject] private Wallet _wallet;

        public override void Init()
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