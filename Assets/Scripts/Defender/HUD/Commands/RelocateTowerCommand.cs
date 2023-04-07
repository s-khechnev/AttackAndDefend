using Defender.HUD.Menus;
using Defender.Towers;
using Defender.Towers.Base;
using Models;
using UnityEngine.UI;

namespace Defender.HUD.Commands
{
    public class RelocateTowerCommand : CommandBase
    {
        private readonly TowerBuilder _towerBuilder;
        private readonly Wallet _wallet;
        
        private BaseTower _towerToRelocate;

        public RelocateTowerCommand(GUIMenuBase panel, TowerBuilder towerBuilder, Wallet wallet) : base(panel)
        {
            _towerBuilder = towerBuilder;
            _wallet = wallet;
        }

        public void SetTower(BaseTower tower)
        {
            _towerToRelocate = tower;
        }

        public override bool CanExecute(Button button)
            => _wallet.IsEnoughMoney(_towerToRelocate.BaseTowerData.Cost) &&
               DefenderGUIManager.GameState == DefenderGameState.Normal;

        public override void Execute(Button button)
        {
            _towerBuilder.RelocateTower(_towerToRelocate);
        }
    }
}