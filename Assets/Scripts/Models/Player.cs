namespace Models
{
    public abstract class Player
    {
        public string Name { get; set; }

        public Player(string name)
        {
            Name = name;
        }
    }
}