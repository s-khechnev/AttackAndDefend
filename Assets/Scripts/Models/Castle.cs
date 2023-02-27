using System;

namespace Models
{
    public class Castle
    {
        public int Health { get; private set; }

        public event Action Lose;
        public event Action<int, int> HealthChanged;

        private readonly int MaxHealth;  
        
        public Castle(int maxHealth)
        {
            Health = maxHealth;
            MaxHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            HealthChanged?.Invoke(Health, MaxHealth);
            
            if (Health < 0)
            {
                Lose?.Invoke();
            }
        }
    }
}