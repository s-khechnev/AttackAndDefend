using System;
using Data.Towers;
using Defender.HUD;
using Defender.Towers;
using Helpers;
using Models;

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

    public event Action<TowerData> StartBuildTower;

    public State CurrentState { get; private set; }
    public bool IsBuilding => CurrentState == State.Building;

    public Wallet Wallet { get; private set; }
    private const int defaultCountMoney = 100; 

    private void Awake()
    {
        Init();
        
        SubscribeEvents();
    }

    private void Init()
    {
        Wallet = new Wallet(defaultCountMoney);
    }

    private void SubscribeEvents()
    {
        HUDManager.Instance.BuildTowerTapped += OnBuildTowerTapped;
        
        TowerBuilder.Instance.TowerBuilt += OnTowerBuilt;
        TowerBuilder.Instance.CancelBuild += OnCancelBuild;
    }

    private void OnCancelBuild(Tower tower)
    {
        SetState(State.Normal);
    }

    private void OnTowerBuilt(Tower tower)
    {
        SetState(State.Normal);
    }

    private void SetState(State newState)
    {
        CurrentState = newState;

        switch (newState)
        {
            case State.Normal:
                break;
            case State.Building:
                break;
        }
    }

    private void OnBuildTowerTapped(TowerData towerData)
    {
        SetState(State.Building);

        StartBuildTower?.Invoke(towerData);
    }
}