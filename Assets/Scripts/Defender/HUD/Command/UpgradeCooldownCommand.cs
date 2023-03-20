using System;
using Defender.HUD.Menu;
using Models;
using UnityEngine.UI;

namespace Defender.HUD.Command
{
    [Serializable]
    public class UpgradeCooldownCommand : TowerUpgradeCommand
    {
        private Wallet _wallet;

        public UpgradeCooldownCommand(GUIMenuBase panel, Wallet wallet) : base(panel)
        {
            _wallet = wallet;
        }

        public override bool CanExecute(Button button)
            => _wallet.IsEnoughMoney(TowerData.Cooldown.CostUpgrade) && TowerData.Cooldown.CanUpgrade;

        public override void Execute(Button button)
        {
            _wallet.Purchase(TowerData.Cooldown.CostUpgrade);
            TowerData.UpgradeCooldown();
        }
    }
}