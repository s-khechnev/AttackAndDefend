using Defender.Towers.Base;

namespace Defender.HUD.Bars
{
    public class AttributeBar : Bar
    {
        private ILevelChanger _attribute;

        public void Init(ILevelChanger attribute)
        {
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