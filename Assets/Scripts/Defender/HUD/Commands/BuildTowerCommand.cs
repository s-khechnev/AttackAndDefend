using Defender.HUD.Menus;
using Defender.Towers;
using UnityEngine.UI;

namespace Defender.HUD.Commands
{
    public class BuildTowerCommand : CommandBase
    {
        private TowerBuilder _towerBuilder;
        private Tower _towerToBuild;

        public BuildTowerCommand(GUIMenuBase panel, TowerBuilder towerBuilder, Tower tower) : base(panel)
        {
            _towerBuilder = towerBuilder;
            _towerToBuild = tower;
        }

        public override bool CanExecute(Button button)
        {
            return true;
        }

        public override void Execute(Button button)
        {
            _towerBuilder.StartBuildTower(_towerToBuild);
        }
    }
}