using Defender.HUD.Menus;
using Defender.Towers;
using Models;
using UnityEngine.UI;

namespace Defender.HUD.Commands
{
    public class RelocateTowerCommand : CommandBase
    {
        private TowerBuilder _towerBuilder;
        private TowerView _towerView;
        private Wallet _wallet;

        public RelocateTowerCommand(GUIMenuBase panel, TowerBuilder towerBuilder, Wallet wallet) : base(panel)
        {
            _towerBuilder = towerBuilder;
            _wallet = wallet;
        }

        public void SetTowerView(TowerView towerView)
        {
            _towerView = towerView;
        }

        public override bool CanExecute(Button button)
            => _wallet.IsEnoughMoney(_towerView.Tower.TowerData.Cost) &&
               DefenderGUIManager.GameState == DefenderGameState.Normal;

        public override void Execute(Button button)
        {
            _towerBuilder.RelocateTower(_towerView);
        }
    }
}