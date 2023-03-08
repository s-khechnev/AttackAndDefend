using UnityEngine;

namespace Data.Towers
{
    [CreateAssetMenu(fileName = "TowerData", menuName = "ScriptableObjects/Tower")]
    public class TowerData : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private int _cost;
        [SerializeField] private float _attackRange;
        [SerializeField] private float _cooldown;

        public string Name => _name;
        public int Cost => _cost;
        public float AttackRange => _attackRange;
        public float Cooldown => _cooldown;
    }
}