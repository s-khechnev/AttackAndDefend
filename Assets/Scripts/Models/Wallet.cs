using System;

namespace Models
{
    public class Wallet
    {
        public event Action<int> MoneyChanged;

        private const int DefaultCountMoney = 100;

        public int Money { get; private set; }

        public Wallet()
        {
            Money = DefaultCountMoney;
        }

        public bool IsEnoughMoney(int cost)
        {
            return Money >= cost;
        }

        public void Purchase(int cost)
        {
            if (Money < cost)
            {
                throw new Exception("Not enough money for purchase");
            }
            
            Money -= cost;
            
            MoneyChanged?.Invoke(Money);
        }
    }
}