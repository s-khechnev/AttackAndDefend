using System;
using UnityEngine;

namespace Data.Towers
{
    [Serializable]
    public abstract class TowerAttribute<T>
    {
        [SerializeField] protected T _value;
        [SerializeField] private int _costUpgrade = 2;
        [SerializeField] protected T _upgradeUnit;
        [SerializeField] private int _maxLevel;
        protected int CurrentLevel;

        public T Value => _value;
        public int CostUpgrade => _costUpgrade;
        public bool CanUpgrade => CurrentLevel < _maxLevel;

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
        public event Action TowerRangeChanged;

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
            TowerRangeChanged?.Invoke();
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