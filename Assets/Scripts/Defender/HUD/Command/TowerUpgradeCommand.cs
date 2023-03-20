using Data.Towers;
using Defender.HUD.Menu;

namespace Defender.HUD.Command
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