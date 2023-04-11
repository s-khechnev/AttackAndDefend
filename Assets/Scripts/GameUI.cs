using System;
using Defender.HUD;
using UnityEngine;
using Zenject;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Camera _uiCamera;
    [SerializeField] private DefenderGameManager _defenderGameManager;

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
        InitDefender();
    }

    private void InitDefender()
    {
        var defenderGUIManager = _instantiator.InstantiatePrefabForComponent<DefenderGameManager>(_defenderGameManager, transform);
        defenderGUIManager.InitCamera(_uiCamera);
    }

    private void InitAttacker()
    {
        throw new NotImplementedException();
    }
}