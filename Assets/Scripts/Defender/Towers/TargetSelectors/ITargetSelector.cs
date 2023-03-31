using System;
using System.Collections.Generic;
using Attackers;

namespace Defender.Towers.TargetSelectors
{
    public interface ITargetSelector
    {
        Func<Attacker, Attacker, Attacker> NewAttackerInRange { get; }
        Func<Attacker, Attacker, ICollection<Attacker>, Attacker> AttackerHealthChanged { get; }
        string Description { get; }

        Attacker GetTarget(ICollection<Attacker> attackersInRange);
    }
}