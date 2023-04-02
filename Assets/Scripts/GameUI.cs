using System;
using Defender.HUD;
using UnityEngine;
using Zenject;

public class GameUI : MonoBehaviour
{
    [SerializeField] private DefenderGUIManager _defenderGUIManager;

    private IInstantiator _instantiator;

    [Inject]
    private void Construct(IInstantiator instantiator)
    {
        _instantiator = instantiator;
    }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
#if UNITY_EDITOR
        InitDefender();
#else
        switch (GameManager.Instance.GameMode)
        {
            case GameMode.Defender:
                InitDefender();
                break;
            case GameMode.Attacker:
                InitAttacker();
                break;
        }
#endif
    }

    private void InitDefender()
    {
        _instantiator.InstantiatePrefab(_defenderGUIManager, transform);
    }

    private void InitAttacker()
    {
        throw new NotImplementedException();
    }
}