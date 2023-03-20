using UnityEngine.UI;

namespace Defender.HUD.Commands
{
    public interface ICommand
    {
        void Execute(Button context);
        bool CanExecute(Button context);
    }
}