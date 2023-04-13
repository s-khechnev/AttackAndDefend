using System;
using UnityEngine;

namespace Models
{
    /// <summary>
    /// Model of castle
    /// </summary>
    public class Castle : MonoBehaviour
    {
        public int Health { get; private set; }

        public event Action Lose;
        public event Action<int, int> HealthChanged;

        private const int MaxHealth = 100;

        private void Awake()
        {
            Health = MaxHealth;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            HealthChanged?.Invoke(Health, MaxHealth);
            
            if (Health <= 0)
            {
                Lose?.Invoke();
            }
        }
    }
}