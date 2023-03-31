using Defender.HUD.Menus;
using Defender.Towers;
using Models;
using UnityEngine.UI;

namespace Defender.HUD.Commands
{
    public class UpgradeAttributeCommand : CommandBase
    {
        private readonly Wallet _wallet;
        private IUpgradable _attribute;

        public UpgradeAttributeCommand(GUIMenuBase panel, Wallet wallet) : base(panel)
        {
            _wallet = wallet;
        }

        public void SetAttribute(IUpgradable attribute)
        {
            _attribute = attribute;
        }

        public override bool CanExecute(Button button)
            => _wallet.IsEnoughMoney(_attribute.CostUpgrade) && _attribute.CanUpgrade;

        public override void Execute(Button button)
        {
            _wallet.Purchase(_attribute.CostUpgrade);
            _attribute.Upgrade();
        }
    }
}