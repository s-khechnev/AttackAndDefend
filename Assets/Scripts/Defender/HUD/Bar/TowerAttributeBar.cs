using Data.Towers;

namespace Defender.HUD.Bar
{
    public class RangeBar<T> : Bar
    {
        private TowerData _towerData;
        private TowerAttribute<float> _range;
        
        public void Init(TowerData towerData)
        {
            _towerData = towerData;
            _range.LevelChanged += OnRangeLevelChanged;
        }

        private void OnRangeLevelChanged(int level, int maxLevel)
        {
            ChangeValue(level, maxLevel);
        }
    }
}