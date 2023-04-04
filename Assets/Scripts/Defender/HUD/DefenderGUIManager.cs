using System.Collections.Generic;
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

    [RequireComponent(typeof(Canvas))]
    public class DefenderGUIManager : MonoBehaviour
    {
        [SerializeField] private WaveMenu _waveMenu;
        [SerializeField] private TowerBuildMenu _towerBuildMenu;
        [SerializeField] private TowerInfoMenu _towerInfoMenu;
        [SerializeField] private TowerUpgradeMenu _towerUpgradeMenu;
        [SerializeField] private MoneyMenu _moneyMenu;
        [SerializeField] private CastleMenu _castleMenu;

        private Canvas _canvas;
        
        private DiContainer _diContainer;

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public static DefenderGameState GameState { get; private set; }

        private void Awake()
        {
            GameState = DefenderGameState.Normal;

            InitMenus();

            _canvas = GetComponent<Canvas>();
        }

        private void InitMenus()
        {
            var menus = new List<GUIMenuBase>
                { _waveMenu, _towerBuildMenu, _towerInfoMenu, _towerUpgradeMenu, _moneyMenu, _castleMenu };

            foreach (var menu in menus)
            {
                _diContainer.Inject(menu);
                menu.Init();
            }
        }

        public static void SetState(DefenderGameState newState)
        {
            GameState = newState;
        }

        public void InitCamera(Camera uiCamera)
        {
            _canvas.renderMode = RenderMode.ScreenSpaceCamera;
            _canvas.worldCamera = uiCamera;
        }
    }
}