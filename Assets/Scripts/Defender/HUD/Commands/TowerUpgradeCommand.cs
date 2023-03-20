using Data.Towers;
using Defender.HUD.Menus;

namespace Defender.HUD.Commands
{
    public abstract class TowerUpgradeCommand : CommandBase
    {
        protected TowerData TowerData;

        protected TowerUpgradeCommand(GUIMenuBase panel) : base(panel)
        {
        }

        public void SetTowerData(TowerData towerData)
        {
            TowerData = towerData;
        }
    }
}