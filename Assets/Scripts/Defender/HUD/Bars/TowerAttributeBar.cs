using Data.Towers;

namespace Defender.HUD.Bars
{
    public class TowerAttributeBar : Bar
    {
        private ILevelChanger _towerAttribute;

        public void Init(ILevelChanger towerAttribute)
        {
            _towerAttribute = towerAttribute;
            _towerAttribute.LevelChanged += OnLevelChanged;
            OnLevelChanged(_towerAttribute.CurrentLevel, _towerAttribute.MaxLevel);
        }

        private void OnLevelChanged(int level, int maxLevel)
        {
            ChangeValue(level, maxLevel);
        }

        private void OnEnable()
        {
            if (_towerAttribute != null)
                _towerAttribute.LevelChanged += OnLevelChanged;
        }

        private void OnDisable()
        {
            _towerAttribute.LevelChanged -= OnLevelChanged;
        }
    }
}