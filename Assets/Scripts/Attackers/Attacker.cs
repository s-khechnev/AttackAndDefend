﻿using System;
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

        [Inject] protected AttackerFactory Factory;

        [SerializeField] private AttackerData _attackerData;

        private AttackerMovement _mover;

        public AttackerData AttackerData => _attackerData;
        public int Health { get; private set; }
        public float DistanceToCastle => _mover.DistanceToCastle;

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
            Factory.Reclaim(this);
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