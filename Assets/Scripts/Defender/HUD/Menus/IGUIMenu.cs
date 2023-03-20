using UnityEngine;
using UnityEngine.UI;

namespace Defender.HUD.Menus
{
    public interface IGUIMenu
    {
        void Show();
        void Hide();
        bool CanShow(GameObject guiItem);
        bool IsShown(GameObject guiItem);
        bool IsAvailable(GameObject guiItem);
        bool CanButtonClick(Button button);
        void ButtonClick(Button button);
        void OnSourceChanged();
    }
}