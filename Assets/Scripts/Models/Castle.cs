using System;
using UnityEngine;

namespace Models
{
    public class Castle : MonoBehaviour
    {
        public int Health { get; private set; }

        public event Action Lose;
        public event Action<int, int> HealthChanged;

        private int _maxHealth;  
        
        public void Init(int castleMaxHealth)
        {
            Health = castleMaxHealth;
            _maxHealth = castleMaxHealth;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            HealthChanged?.Invoke(Health, _maxHealth);
            
            if (Health < 0)
            {
                Lose?.Invoke();
            }
        }
    }
}