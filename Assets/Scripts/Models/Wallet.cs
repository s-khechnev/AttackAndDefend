namespace Models
{
    public class Wallet
    {
        public int Money { get; private set; }

        public Wallet(int defaultCountMoney)
        {
            Money = defaultCountMoney;

            Init();
        }

        private void Init()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            
        }
    }
}