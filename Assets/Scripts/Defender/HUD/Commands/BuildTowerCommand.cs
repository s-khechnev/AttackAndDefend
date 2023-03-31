using Defender.HUD.Menus;
using Defender.Towers;
using Models;
using UnityEngine.UI;

namespace Defender.HUD.Commands
{
    public class BuildTowerCommand : CommandBase
    {
        private readonly TowerBuilder _towerBuilder;
        private readonly Tower _towerToBuild;
        private readonly Wallet _wallet;

        public BuildTowerCommand(GUIMenuBase panel, TowerBuilder towerBuilder, Tower tower, Wallet wallet) : base(panel)
        {
            _towerBuilder = towerBuilder;
            _towerToBuild = tower;
            _wallet = wallet;
        }

        public override bool CanExecute(Button button)
            => _wallet.IsEnoughMoney(_towerToBuild.TowerData.Cost) &&
               DefenderGUIManager.GameState == DefenderGameState.Normal;

        public override void Execute(Button button)
        {
            _towerBuilder.StartBuildTower(_towerToBuild);
        }
    }
}