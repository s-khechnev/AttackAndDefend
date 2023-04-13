using System.Collections.Generic;
using System.Linq;
using Attackers;

namespace Defender.Towers.TargetSelectors
{
    public class NearestTargetSelector : ITargetSelector
    {
        public string Description => "Nearest";

        public Attacker GetTarget(ICollection<Attacker> attackersInRange)
        {
            if (attackersInRange.Count == 0)
                return null;

            var nearest = attackersInRange.First();
            foreach (var attacker in attackersInRange)
            {
                if (attacker.DistanceToCastle < nearest.DistanceToCastle)
                    nearest = attacker;
            }

            return nearest;
        }

        public Attacker NewAttackerInRange(Attacker currentTarget, Attacker newAttacker)
        {
            if (currentTarget == null || newAttacker.DistanceToCastle < currentTarget.DistanceToCastle)
                return newAttacker;

            return currentTarget;
        }

        public Attacker AttackerHealthChanged(Attacker currentTarget, Attacker changedHealth,
            ICollection<Attacker> inRange)
        {
            return currentTarget;
        }
    }
}