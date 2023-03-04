using Defender.Towers;
using UnityEngine;

namespace Data.Towers
{
    public enum TowerType
    {
        Common,
    }

    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Tower")]
    public class TowerData : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Tower _towerPrefab;
        [SerializeField] private TowerType _type;
        [SerializeField] private int _cost;
        [SerializeField] private float _attackRange;

        public string Name => _name;
        public Tower TowerPrefab => _towerPrefab;
        public TowerType Type => _type;
        public int Cost => _cost;
        public float AttackRange => _attackRange;
    }
}