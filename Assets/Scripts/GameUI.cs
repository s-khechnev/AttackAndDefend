using System;
using Defender.HUD;
using UnityEngine;
using Zenject;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Camera _uiCamera;
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
        InitDefender();
    }

    private void InitDefender()
    {
        var defenderGUIManager = _instantiator.InstantiatePrefabForComponent<DefenderGUIManager>(_defenderGUIManager, transform);
        defenderGUIManager.InitCamera(_uiCamera);
    }

    private void InitAttacker()
    {
        throw new NotImplementedException();
    }
}