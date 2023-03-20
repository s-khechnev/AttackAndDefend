using UnityEngine.UI;

namespace Defender.HUD.Command
{
    public interface ICommand
    {
        void Execute(Button context);
        bool CanExecute(Button context);
    }
}