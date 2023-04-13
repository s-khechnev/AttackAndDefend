using System;
using UnityEngine;

namespace Defender.Towers.Base
{
    /// <summary>
    /// Attribute of the tower
    /// </summary>
    [Serializable]
    public class Attribute : ILevelChanger
    {
        public event Action<int, int> LevelChanged;
        public event Action<float> ValueChanged;

        [SerializeField] private string _description;
        [SerializeField] private float _value;
        [SerializeField] private int _costUpgrade = 2;
        [SerializeField] protected float _upgradeUnit;
        [SerializeField] private int _maxLevel;

        private int _currentLevel;

        public int CurrentLevel
        {
            get => _currentLevel;
            protected set
            {
                _currentLevel = value;
                LevelChanged?.Invoke(_currentLevel, _maxLevel);
            }
        }

        public float Value
        {
            get => _value;
            protected set
            {
                _value = value;
                ValueChanged?.Invoke(_value);
            }
        }

        public string Description => _description;
        public int CostUpgrade => _costUpgrade;
        public bool CanUpgrade => CurrentLevel < _maxLevel;
        public int MaxLevel => _maxLevel;

        public void Upgrade()
        {
            if (!CanUpgrade)
                throw new Exception("Already max level");

            Value += _upgradeUnit;
            CurrentLevel++;
        }
    }
}