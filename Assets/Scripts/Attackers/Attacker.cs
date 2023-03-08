using System;
using Data.Attackers;
using Models;
using UnityEngine;
using Zenject;

namespace Attackers
{
    public abstract class Attacker : MonoBehaviour
    {
        public event Action Died;
        public event Action<int, int> HealthChanged;

        [Inject] protected AttackerFactory Factory;

        [SerializeField] private AttackerData _attackerData;

        public AttackerData AttackerData => _attackerData;
        public int Health { get; private set; }

        private void Awake()
        {
            Health = _attackerData.Health;
        }

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

        public void TakeDamage(int damage)
        {
            Health -= damage;
            HealthChanged?.Invoke(Health, _attackerData.Health);

            if (Health <= 0)
            {
                Died?.Invoke();
                Factory.Reclaim(this);
            }
        }
    }
}