using Defender.HUD;
using UnityEngine;
using Zenject;

public class GameUI : MonoBehaviour
{
    [SerializeField] private DefenderHUD _defenderHUD;

    [Inject] private IInstantiator _instantiator;

    private void Awake()
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
        _instantiator.InstantiatePrefab(_defenderHUD);
    }
}