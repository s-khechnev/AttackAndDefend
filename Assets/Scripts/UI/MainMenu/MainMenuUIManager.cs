using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class MainMenuUIManager : MonoBehaviour
    {
        [SerializeField] private Button _startGameButton;

        private void OnEnable()
        {
            _startGameButton.onClick.AddListener(OnStartGameButtonClick);
        }

        private void OnDisable()
        {
            _startGameButton.onClick.RemoveListener(OnStartGameButtonClick);
        }

        private void OnStartGameButtonClick()
        {
            SceneManager.LoadSceneAsync("Lobby");
        }
    }
}