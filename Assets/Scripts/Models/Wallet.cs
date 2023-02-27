using System;

namespace Models
{
    public class Wallet
    {
        public int Money { get; private set; }
        
        public event Action<int> MoneyChanged; 

        public Wallet(int defaultCountMoney)
        {
            Money = defaultCountMoney;
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