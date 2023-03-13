using System;
using UnityEngine;

namespace Data.Towers
{
    [Serializable]
    public class TowerAttribute
    {
        [SerializeField] private float _value;
        [SerializeField] private int _costUpgrade = 2;
        [SerializeField] private float _upgradeUnit;
        [SerializeField] private int _maxLevel;
        private int _currentLevel;

        public float Value => _value;
        public int CostUpgrade => _costUpgrade;
        public bool CanUpgrade => _currentLevel < _maxLevel;

        public void Upgrade()
        {
            if (!CanUpgrade)
                throw new Exception("Already max level");
            
            _value += _upgradeUnit;
            _currentLevel++;
        }
    }
    
    [CreateAssetMenu(fileName = "TowerData", menuName = "ScriptableObjects/Tower")]
    public class TowerData : ScriptableObject
    {
        public event Action TowerRangeChanged; 

        [SerializeField] private string _name;
        [SerializeField] private int _cost;
        
        [SerializeField] private TowerAttribute _attackRange;
        [SerializeField] private TowerAttribute _cooldown;
        [SerializeField] private TowerAttribute _damage;

        public string Name => _name;
        public int Cost => _cost;

        public TowerAttribute Range => _attackRange;
        public TowerAttribute Cooldown => _cooldown;
        public TowerAttribute Damage => _damage;

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