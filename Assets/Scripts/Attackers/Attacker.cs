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
        
        /// <summary>
        /// Data of attacker
        /// </summary>
        public AttackerData AttackerData => _attackerData;
        
        /// <summary>
        /// Remaining distance to the castle
        /// </summary>
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

        /// <summary>
        /// Attack the castle
        /// </summary>
        /// <param name="castle">castle to attack</param>
        protected virtual void Attack(Castle castle)
        {
            castle.TakeDamage(AttackerData.Damage);
            _attackerFactory.Destroy(this);
        }

        /// <summary>
        /// Take the damage
        /// </summary>
        /// <param name="damage">damage to take</param>
        public void TakeDamage(int damage)
        {
            Health -= damage;
            HealthChanged?.Invoke(this, Health, _attackerData.Health);

            if (Health <= 0)
            {
                _attackerFactory.Destroy(this);
            }
        }

        private void OnDestroy()
        {
            Died?.Invoke(this);
        }
    }
}