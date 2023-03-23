using System;
using UnityEngine;

namespace Defender.Towers
{
    public interface ILevelChanger
    {
        public event Action<int, int> LevelChanged;
        public int CurrentLevel { get; }
        public int MaxLevel { get; }
    }

    [Serializable]
    public abstract class TowerAttribute<T> : ILevelChanger
    {
        public event Action<int, int> LevelChanged;

        [SerializeField] protected T _value;
        [SerializeField] private int _costUpgrade = 2;
        [SerializeField] protected T _upgradeUnit;
        [SerializeField] private int _maxLevel;

        private int _currentLevel;

        public int CurrentLevel
        {
            get => _currentLevel;
            set
            {
                _currentLevel = value;
                LevelChanged?.Invoke(_currentLevel, _maxLevel);
            }
        }
        
        public T Value => _value;
        public int CostUpgrade => _costUpgrade;
        public bool CanUpgrade => CurrentLevel < _maxLevel;
        public int MaxLevel => _maxLevel;

        public abstract void Upgrade();
    }

    [Serializable]
    public class IntTowerAttribute : TowerAttribute<int>
    {
        public override void Upgrade()
        {
            if (!CanUpgrade)
                throw new Exception("Already max level");

            _value += _upgradeUnit;
            CurrentLevel++;
        }
    }

    [Serializable]
    public class FloatTowerAttribute : TowerAttribute<float>
    {
        public override void Upgrade()
        {
            if (!CanUpgrade)
                throw new Exception("Already max level");

            _value += _upgradeUnit;
            CurrentLevel++;
        }
    }

    [CreateAssetMenu(fileName = "TowerData", menuName = "ScriptableObjects/Tower")]
    public class TowerData : ScriptableObject
    {
        public event Action<float> RangeValueChanged;

        [SerializeField] private string _name;
        [SerializeField] private int _cost;
        [SerializeField] private FloatTowerAttribute _attackRange;
        [SerializeField] private FloatTowerAttribute _cooldown;
        [SerializeField] private IntTowerAttribute _damage;

        public string Name => _name;
        public int Cost => _cost;

        public FloatTowerAttribute Range => _attackRange;
        public FloatTowerAttribute Cooldown => _cooldown;
        public IntTowerAttribute Damage => _damage;

        public void UpgradeRange()
        {
            _attackRange.Upgrade();
            RangeValueChanged?.Invoke(Range.Value);
        }

        public void UpgradeCooldown()
        {
            _cooldown.Upgrade();
        }

        public void UpgradeDamage()
        {
            _damage.Upgrade();
        }
    }
}