using Data.Attackers;
using Models;
using UnityEngine;
using Zenject;

namespace Attackers
{
    public abstract class Attacker : MonoBehaviour
    {
        [Inject] protected AttackerFactory Factory;

        [SerializeField] private AttackerData _attackerData;

        public AttackerData AttackerData => _attackerData;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Castle castle))
            {
                Attack(castle);
            }
        }

        protected virtual void Attack(Castle castle)
        {
            castle.TakeDamage(AttackerData.Damage);
            Factory.Reclaim(this);
        }
    }
}