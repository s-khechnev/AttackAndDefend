using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Lobby
{
    public class LobbyUIManager : MonoBehaviour
    {
        public static LobbyUIManager Instance { get; private set; }

        public event Action<GameMode> StartTapped;

        [SerializeField] private Button _startButton;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void OnEnable()
        {
            _startButton.onClick.AddListener(OnStartClick);
        }

        private void OnStartClick()
        {
            StartTapped?.Invoke(GameMode.Defender);
            SceneManager.LoadScene("GameScene");
        }
    }
}