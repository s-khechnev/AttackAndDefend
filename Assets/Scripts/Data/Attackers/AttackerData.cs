using UnityEngine;

namespace Data.Attackers
{
    public enum AttackerType
    {
        Common
    }
    
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Attacker")]
    public class AttackerData : ScriptableObject
    {
        [SerializeField] private int _damage;
        [SerializeField] private int _speed;
        [SerializeField] private global::Attackers.Attacker _prefab;
        [SerializeField] private AttackerType _attackerType;
        
        public int Damage => _damage;
        public int Speed => _speed;
        public global::Attackers.Attacker Prefab => _prefab;
        public AttackerType Type => _attackerType;
    }
}