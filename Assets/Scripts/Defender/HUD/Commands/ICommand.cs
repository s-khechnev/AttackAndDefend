using UnityEngine.UI;

namespace Defender.HUD.Commands
{
    public interface ICommand
    {
        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="context">context of execution</param>
        void Execute(Button context);
        
        /// <summary>
        /// Get the possibility of executing
        /// </summary>
        /// <param name="context">context of execution</param>
        /// <returns>The possibility of executing</returns>
        bool CanExecute(Button context);
    }
}