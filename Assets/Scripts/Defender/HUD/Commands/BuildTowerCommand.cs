using Defender.HUD.Menus;
using Defender.Towers;
using Models;
using UnityEngine.UI;

namespace Defender.HUD.Commands
{
    public class BuildTowerCommand : CommandBase
    {
        private readonly TowerBuilder _towerBuilder;
        private readonly BuildTowerButton _towerButton;
        private readonly Wallet _wallet;

        public BuildTowerCommand(GUIMenuBase panel, TowerBuilder towerBuilder, BuildTowerButton towerButton, Wallet wallet) : base(panel)
        {
            _towerBuilder = towerBuilder;
            _towerButton = towerButton;
            _wallet = wallet;
        }

        public override bool CanExecute(Button button)
            => _wallet.IsEnoughMoney(_towerButton.Tower.TowerData.Cost) &&
               DefenderGUIManager.GameState == DefenderGameState.Normal;

        public override void Execute(Button button)
        {
            _towerBuilder.StartBuildTower(_towerButton.TowerView);
        }
    }
}