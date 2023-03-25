using Defender.HUD.Menus;
using UnityEngine;
using Zenject;

namespace Defender.HUD
{
    public enum DefenderGameState
    {
        Normal,
        Pause,
        Building
    }

    public class DefenderGUIManager : MonoBehaviour
    {
        [SerializeField] private WaveMenu _waveMenu;
        [SerializeField] private TowerBuildMenu _towerBuildMenu;
        [SerializeField] private TowerUpgradeMenu _towerUpgradeMenu;
        [SerializeField] private MoneyMenu _moneyMenu;
        [SerializeField] private CastleMenu _castleMenu;

        [Inject] private DiContainer _diContainer;

        public static DefenderGameState GameState { get; private set; }

        private void Awake()
        {
            GameState = DefenderGameState.Normal;

            InitWaveMenu();
            InitTowerBuildMenu();
            InitTowerUpgradeMenu();
            InitMoneyMenu();
            InitCastleMenu();
        }

        private void InitWaveMenu()
        {
            _diContainer.Inject(_waveMenu);
            _waveMenu.Init();
        }

        private void InitTowerBuildMenu()
        {
            _diContainer.Inject(_towerBuildMenu);
            _towerBuildMenu.Init();
        }

        private void InitTowerUpgradeMenu()
        {
            _diContainer.Inject(_towerUpgradeMenu);
            _towerUpgradeMenu.Init();
        }

        private void InitMoneyMenu()
        {
            _diContainer.Inject(_moneyMenu);
            _moneyMenu.Init();
        }

        private void InitCastleMenu()
        {
            _diContainer.Inject(_castleMenu);
            _castleMenu.Init();
        }

        public static void SetState(DefenderGameState newState)
        {
            GameState = newState;
        }
    }
}