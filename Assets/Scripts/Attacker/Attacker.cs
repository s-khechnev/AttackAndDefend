using Data.Attackers;
using Models;
using UnityEngine;
using Zenject;

namespace Attacker
{
    public abstract class Attacker : MonoBehaviour
    {
        [Inject] protected AttackerFactory Factory;
        public abstract AttackerType Type { get; }
        public AttackerData AttackerData => Factory.GetAttackerData(Type);

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
        }
    }
}