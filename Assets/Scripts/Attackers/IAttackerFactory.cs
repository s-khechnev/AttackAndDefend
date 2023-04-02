using System;
using Factories;

namespace Attackers
{
    public interface IAttackerFactory : IFactory<Attacker>
    {
        public event Action<Attacker> AttackerDied;
        public int CountAttackers { get; }
    }
}