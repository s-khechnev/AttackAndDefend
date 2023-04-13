using Defender.HUD.Menus;
using UnityEngine.UI;

namespace Defender.HUD.Commands
{
    public abstract class CommandBase : ICommand
    {
        /// <summary>
        /// The menu which contains button which associate with that command
        /// </summary>
        protected readonly GUIMenuBase Panel;

        protected CommandBase(GUIMenuBase panel)
        {
            Panel = panel;
        }

        public abstract bool CanExecute(Button button);

        public abstract void Execute(Button button);
    }
}