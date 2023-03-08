using UnityEngine;

namespace Data.Attackers
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Attacker")]
    public class AttackerData : ScriptableObject
    {
        [SerializeField] private int _damage;
        [SerializeField] private int _speed;
        [SerializeField] private int _health;

        public int Damage => _damage;
        public int Speed => _speed;
        public int Health => _health;
    }
}