using Defender.HUD.Menus;
using Defender.Towers;
using TMPro;
using UnityEngine.UI;

namespace Defender.HUD.Commands
{
    public class ChangeTargetSelectorCommand : CommandBase
    {
        private readonly TMP_Text _selectorDescription;
        
        private TargetFinder _targetFinder;

        public ChangeTargetSelectorCommand(GUIMenuBase panel, TMP_Text selectorDescription) : base(panel)
        {
            _selectorDescription = selectorDescription;
        }
        
        public void SetTargetFinder(TargetFinder targetFinder)
        {
            _targetFinder = targetFinder;
            _selectorDescription.text = _targetFinder.GetSelectorDescription();
        }

        public override bool CanExecute(Button button)
        {
            return true;
        }

        public override void Execute(Button button)
        {
            _targetFinder.ChangeSelector();
            _selectorDescription.text = _targetFinder.GetSelectorDescription();
        }
    }
}