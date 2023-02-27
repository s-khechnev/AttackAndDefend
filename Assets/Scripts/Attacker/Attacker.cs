using Data.Attackers;
using Models;
using UnityEngine;

namespace Attacker
{
    public class Attacker : MonoBehaviour
    {
        [SerializeField] private AttackerData _attackerData;

        public AttackerData AttackerData => _attackerData;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Castle castle))
            {
                Attack(castle);
            }
        }

        private void Attack(Castle castle)
        {
            castle.TakeDamage(_attackerData.Damage);
        }
    }
}