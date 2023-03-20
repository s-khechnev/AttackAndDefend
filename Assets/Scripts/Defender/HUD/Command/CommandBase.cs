using Defender.HUD.Menu;
using UnityEngine.UI;

namespace Defender.HUD.Command
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