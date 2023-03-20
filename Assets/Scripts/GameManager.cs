using UI.Lobby;
using UnityEngine;

public enum GameMode
{
    Attacker,
    Defender
}

public class GameManager : MonoBehaviour
{
    public GameMode GameMode { get; private set; }

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        LobbyUIManager.Instance.StartTapped += OnStartTapped;
    }

    private void OnStartTapped(GameMode gameMode)
    {
        GameMode = gameMode;
    }
}