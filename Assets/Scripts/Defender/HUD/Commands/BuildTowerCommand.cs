using Defender.HUD.Menus;
using Defender.Towers;
using Defender.Towers.Base;
using Models;
using UnityEngine.UI;

namespace Defender.HUD.Commands
{
    public class BuildTowerCommand : CommandBase
    {
        private readonly TowerBuilder _towerBuilder;
        private readonly BaseTower _towerToBuild;
        private readonly Wallet _wallet;

        public BuildTowerCommand(GUIMenuBase panel, TowerBuilder towerBuilder, BaseTower towerToBuild, Wallet wallet) :
            base(panel)
        {
            _towerBuilder = towerBuilder;
            _towerToBuild = towerToBuild;
            _wallet = wallet;
        }

        public override bool CanExecute(Button button)
            => _wallet.IsEnoughMoney(_towerToBuild.BaseTowerData.Cost) &&
               DefenderGUIManager.GameState == DefenderGameState.Normal;

        public override void Execute(Button button)
        {
            _towerBuilder.StartBuildTower(_towerToBuild);
        }
    }
}