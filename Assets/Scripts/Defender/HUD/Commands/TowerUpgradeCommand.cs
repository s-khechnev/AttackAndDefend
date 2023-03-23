using Defender.HUD.Menus;
using Defender.Towers;

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