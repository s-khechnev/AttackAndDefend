using System;

namespace Models
{
    /// <summary>
    /// Model of player's wallet
    /// </summary>
    public class Wallet
    {
        public event Action<int> MoneyChanged;

        public int Money
        {
            get => _money;
            private set
            {
                if (_money == value)
                    return;

                _money = value;
                MoneyChanged?.Invoke(_money);
            }
        }

        private int _money;

        private const int DefaultCountMoney = 100;
        
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
                throw new Exception("Not enough money for purchase");

            AddMoney(-cost);
        }

        public void AddMoney(int amount)
        {
            Money += amount;
        }
    }
}