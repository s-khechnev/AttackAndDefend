namespace Models
{
    public class DefenderPlayer : Player
    {
        public Wallet Wallet { get; private set; }

        public DefenderPlayer(string name, Wallet wallet) : base(name)
        {
            Wallet = wallet;
        }
    }
}