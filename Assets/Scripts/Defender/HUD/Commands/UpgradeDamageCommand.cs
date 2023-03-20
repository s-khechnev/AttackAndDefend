using Defender.HUD.Menus;
using Models;
using UnityEngine.UI;

namespace Defender.HUD.Commands
{
    public class UpgradeDamageCommand : TowerUpgradeCommand
    {
        private Wallet _wallet;

        public UpgradeDamageCommand(GUIMenuBase panel, Wallet wallet) : base(panel)
        {
            _wallet = wallet;
        }

        public override bool CanExecute(Button button)
            => _wallet.IsEnoughMoney(TowerData.Damage.CostUpgrade) && TowerData.Damage.CanUpgrade;

        public override void Execute(Button button)
        {
            _wallet.Purchase(TowerData.Damage.CostUpgrade);
            TowerData.UpgradeDamage();
        }
    }
}