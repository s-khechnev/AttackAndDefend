using Data.Towers;

namespace Defender.HUD.Bar
{
    public class TowerAttributeBar : Bar
    {
        private ILevelChanger _levelChanger;

        public void Init(ILevelChanger levelChanger)
        {
            _levelChanger = levelChanger;
            _levelChanger.LevelChanged += OnLevelChanged;
            OnLevelChanged(_levelChanger.CurrentLevel, _levelChanger.MaxLevel);
        }

        private void OnLevelChanged(int level, int maxLevel)
        {
            ChangeValue(level, maxLevel);
        }
    }
}