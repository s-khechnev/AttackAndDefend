using UnityEngine;

namespace Data.Attackers
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Attacker")]
    public class AttackerData : ScriptableObject
    {
        [SerializeField] private int _damage;
        [SerializeField] private int _speed;
        [SerializeField] private GameObject _prefab;

        public GameObject Prefab => _prefab;
        public int Damage => _damage;
        public int Speed => _speed;
    }
}