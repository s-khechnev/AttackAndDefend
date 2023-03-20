using Defender.HUD.Menus;
using UnityEngine.UI;

namespace Defender.HUD.Commands
{
    public abstract class CommandBase : ICommand
    {
        protected GUIMenuBase Panel;

        public CommandBase(GUIMenuBase panel)
        {
            Panel = panel;
        }

        public abstract bool CanExecute(Button button);

        public abstract void Execute(Button button);
    }
}