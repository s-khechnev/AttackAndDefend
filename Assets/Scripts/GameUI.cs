using Defender.HUD;
using UnityEngine;
using Zenject;

public class GameUI : MonoBehaviour
{
    [Inject] private DefenderHUD _defenderHUD;

    private void Awake()
    {
        _defenderHUD.gameObject.SetActive(false);
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
#if UNITY_EDITOR
        InitDefender();
#else
        if (GameManager.Instance.GameMode == GameMode.Defender)
            InitDefender();
#endif
    }

    private void InitDefender()
    {
        _defenderHUD.gameObject.SetActive(true);
    }
}