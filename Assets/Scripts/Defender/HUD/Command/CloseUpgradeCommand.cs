using System;
using Defender.HUD.Menu;
using UnityEngine.UI;

namespace Defender.HUD.Command
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