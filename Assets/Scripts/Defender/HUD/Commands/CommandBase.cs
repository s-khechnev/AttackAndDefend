using Defender.HUD.Menus;
using UnityEngine.UI;

namespace Defender.HUD.Commands
{
    public abstract class CommandBase : ICommand
    {
        protected readonly GUIMenuBase Panel;

        protected CommandBase(GUIMenuBase panel)
        {
            Panel = panel;
        }

        public abstract bool CanExecute(Button button);

        public abstract void Execute(Button button);
    }
}