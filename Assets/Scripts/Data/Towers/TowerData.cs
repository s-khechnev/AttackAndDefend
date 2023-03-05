using UnityEngine;

namespace Data.Towers
{
    [CreateAssetMenu(fileName = "TowerData", menuName = "ScriptableObjects/Tower")]
    public class TowerData : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private int _cost;
        [SerializeField] private float _attackRange;

        public string Name => _name;
        public int Cost => _cost;
        public float AttackRange => _attackRange;
    }
}