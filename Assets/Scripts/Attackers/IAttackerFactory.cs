using System;
using Factories;

namespace Attackers
{
    public interface IAttackerFactory : IFactory<Attacker>
    {
        public event Action<Attacker> AttackerDied;
        
        /// <summary>
        /// Count of attackers on scene at the moment
        /// </summary>
        public int CountAttackers { get; }
    }
}