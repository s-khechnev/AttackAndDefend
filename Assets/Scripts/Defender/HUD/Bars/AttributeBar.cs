using Defender.Towers.Base;

namespace Defender.HUD.Bars
{
    public class AttributeBar : Bar
    {
        /// <summary>
        /// The attribute that is currently displayed
        /// </summary>
        private ILevelChanger _attribute;

        /// <summary>
        /// Initialize the bar
        /// </summary>
        /// <param name="attribute">attribute to display</param>
        public void Init(ILevelChanger attribute)
        {
            if (_attribute != null)
                _attribute.LevelChanged -= OnLevelChanged;
            
            _attribute = attribute;
            _attribute.LevelChanged += OnLevelChanged;
            OnLevelChanged(_attribute.CurrentLevel, _attribute.MaxLevel);
        }

        private void OnLevelChanged(int level, int maxLevel)
        {
            ChangeValue(level, maxLevel);
        }

        private void OnEnable()
        {
            if (_attribute != null)
                _attribute.LevelChanged += OnLevelChanged;
        }

        private void OnDisable()
        {
            if (_attribute != null)
                _attribute.LevelChanged -= OnLevelChanged;
        }
    }
}