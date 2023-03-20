using System;
using Defender.HUD.Menu;
using Models;
using UnityEngine.UI;

namespace Defender.HUD.Command
{
    [Serializable]
    public class UpgradeRangeCommand : TowerUpgradeCommand
    {
        private Wallet _wallet;

        public UpgradeRangeCommand(GUIMenuBase panel, Wallet wallet) : base(panel)
        {
            _wallet = wallet;
        }

        public override bool CanExecute(Button button)
            => _wallet.IsEnoughMoney(TowerData.Range.CostUpgrade) && TowerData.Range.CanUpgrade;

        public override void Execute(Button button)
        {
            _wallet.Purchase(TowerData.Range.CostUpgrade);
            TowerData.UpgradeRange();
        }
    }
}