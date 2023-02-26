using System;
using Data.Towers;
using Helpers;

namespace Defender.HUD
{
    public class HUDManager : Singleton<HUDManager>
    {
        private BuildTowerButton[] _buildTowerButtons;

        public event Action<TowerData> BuildTowerTapped;
        
        private void Awake()
        {
            _buildTowerButtons = FindObjectsOfType<BuildTowerButton>();

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            foreach (var button in _buildTowerButtons)
            {
                button.BuildTowerClick += BuildTowerClick;
            }
        }

        private void BuildTowerClick(TowerData towerData)
        {
            BuildTowerTapped?.Invoke(towerData);
        }
    }
}
