using Defender.HUD.Menus;
using UnityEngine.UI;

namespace Defender.HUD.Commands
{
    public class CloseUpgradeCommand : CommandBase
    {
        public CloseUpgradeCommand(GUIMenuBase panel) : base(panel)
        {
        }

        public override bool CanExecute(Button button)
        {
            return true;
        }

        public override void Execute(Button button)
        {
            Panel.Hide();
        }
    }
}