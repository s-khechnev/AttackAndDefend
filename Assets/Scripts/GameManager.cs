using System;
using Data.Towers;
using Defender.HUD;
using Defender.Towers;
using Helpers;
using Models;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum State
    {
        Normal,
        Building,
        Paused,
        GameOver,
        Victory
    }

    public State GameState { get; private set; }
    public event Action<TowerData> StartBuildTower;
    public event Action<int> MoneyChanged;
    public event Action<int, int> CastleHealthChanged;

    [SerializeField] private TowerBuilder _towerBuilder;
    [SerializeField] private HUDManager _hudManager;
    [SerializeField] private Castle _castle;

    private Wallet _wallet;

    private const int DefaultCountMoney = 10;
    private const int CastleMaxHealth = 100;

    private void Awake()
    {
        InitModels();

        SubscribeEvents();
    }

    private void Start()
    {
        InitHUD();
    }

    private void InitHUD()
    {
        MoneyChanged?.Invoke(_wallet.Money);
        CastleHealthChanged?.Invoke(_castle.Health, CastleMaxHealth);
    }

    private void InitModels()
    {
        _wallet = new Wallet(DefaultCountMoney);

        _castle.Init(CastleMaxHealth);
    }

    private void SubscribeEvents()
    {
        _hudManager.BuildTowerTapped += OnStartBuildTower;

        _towerBuilder.TowerBuilt += OnTowerBuilt;
        _towerBuilder.CancelBuild += OnCancelBuild;

        _wallet.MoneyChanged += OnMoneyChanged;

        _castle.HealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(int value, int maxValue)
    {
        CastleHealthChanged?.Invoke(value, maxValue);
    }

    private void OnMoneyChanged(int money)
    {
        MoneyChanged?.Invoke(money);
    }

    private void OnCancelBuild(Tower tower)
    {
        if (GameState == State.Building)
        {
            SetState(State.Normal);
        }
        else
        {
            throw new Exception("State exception");
        }
    }

    private void OnTowerBuilt(Tower tower)
    {
        if (GameState == State.Building)
        {
            _wallet.Purchase(tower.TowerData.Cost);
            SetState(State.Normal);
        }
        else
        {
            throw new Exception("State exception");
        }
    }

    private void SetState(State newState)
    {
        GameState = newState;

        switch (newState)
        {
            case State.Normal:
                break;
            case State.Building:
                break;
        }
    }

    private void OnStartBuildTower(TowerData towerData)
    {
        if (GameState == State.Normal)
        {
            if (_wallet.IsEnoughMoney(towerData.Cost))
            {
                SetState(State.Building);

                StartBuildTower?.Invoke(towerData);
            }
        }
        else
        {
            throw new Exception("State exception");
        }
    }
}