using Defender.HUD.Menus;
using Defender.Towers;
using Models;
using UnityEngine.UI;

namespace Defender.HUD.Commands
{
    public class RelocateTowerCommand : CommandBase
    {
        private readonly TowerBuilder _towerBuilder;
        private readonly Wallet _wallet;
        
        private Tower _tower;

        public RelocateTowerCommand(GUIMenuBase panel, TowerBuilder towerBuilder, Wallet wallet) : base(panel)
        {
            _towerBuilder = towerBuilder;
            _wallet = wallet;
        }

        public void SetTower(Tower tower)
        {
            _tower = tower;
        }

        public override bool CanExecute(Button button)
            => _wallet.IsEnoughMoney(_tower.TowerData.Cost) &&
               DefenderGUIManager.GameState == DefenderGameState.Normal;

        public override void Execute(Button button)
        {
            _towerBuilder.RelocateTower(_tower.TowerView);
        }
    }
}