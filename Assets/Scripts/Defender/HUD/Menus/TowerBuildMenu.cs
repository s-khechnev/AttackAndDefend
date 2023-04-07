using Defender.HUD.Commands;
using Defender.Towers;
using Models;
using UnityEngine;
using Zenject;

namespace Defender.HUD.Menus
{
    public class TowerBuildMenu : GUIMenuBase
    {
        [SerializeField] private TowerBuilder _towerBuilder;
        
        private BuildTowerButton[] _buildTowerButtons;

        private Wallet _wallet;

        [Inject]
        private void Construct(Wallet wallet)
        {
            _wallet = wallet;
        }

        private void Awake()
        {
            _buildTowerButtons = FindObjectsOfType<BuildTowerButton>();
            
            foreach (var button in _buildTowerButtons)
            {
                var buildTowerCommand = new BuildTowerCommand(this, _towerBuilder, button.Tower, _wallet);
                AssociateButton(button, buildTowerCommand);
            }
        }
    }
}