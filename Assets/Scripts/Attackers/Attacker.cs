using System;
using Attackers.Movement;
using Models;
using UnityEngine;
using Zenject;

namespace Attackers
{
    public abstract class Attacker : MonoBehaviour
    {
        public event Action<Attacker> Died;
        public event Action<Attacker, int, int> HealthChanged;
        
        [SerializeField] private AttackerData _attackerData;
        
        private AttackerMovement _mover;
        private IAttackerFactory _attackerFactory;
        
        public int Health { get; private set; }
        public AttackerData AttackerData => _attackerData;
        public float DistanceToCastle => _mover.DistanceToCastle;

        [Inject]
        private void Construct(IAttackerFactory attackerFactory)
        {
            _attackerFactory = attackerFactory;
        }

        private void Awake()
        {
            _mover = GetComponent<AttackerMovement>();
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
            _attackerFactory.Destroy(this);
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            HealthChanged?.Invoke(this, Health, _attackerData.Health);

            if (Health <= 0)
            {
                Died?.Invoke(this);
            }
        }
    }
}