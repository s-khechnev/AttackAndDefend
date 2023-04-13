using System.Collections.Generic;
using System.Linq;
using Attackers;

namespace Defender.Towers.TargetSelectors
{
    public class MaxHealthTargetSelector : ITargetSelector
    {
        public string Description => "Max health";

        public Attacker GetTarget(ICollection<Attacker> attackersInRange)
        {
            if (attackersInRange.Count == 0)
                return null;

            var maxHealthAttacker = attackersInRange.First();
            foreach (var attacker in attackersInRange)
            {
                if (attacker.Health > maxHealthAttacker.Health)
                {
                    maxHealthAttacker = attacker;
                }
                else if (attacker.Health == maxHealthAttacker.Health &&
                         attacker.DistanceToCastle < maxHealthAttacker.DistanceToCastle)
                {
                    maxHealthAttacker = attacker;
                }
            }

            return maxHealthAttacker;
        }

        public Attacker NewAttackerInRange(Attacker currentTarget, Attacker newAttacker)
        {
            if (currentTarget == null)
                return newAttacker;

            if (currentTarget.Health < newAttacker.Health)
                return newAttacker;

            if (currentTarget.Health == newAttacker.Health &&
                currentTarget.DistanceToCastle > newAttacker.DistanceToCastle)
            {
                return newAttacker;
            }

            return currentTarget;
        }

        public Attacker AttackerHealthChanged(Attacker currentTarget, Attacker changedHealth,
            ICollection<Attacker> attackersInRange)
        {
            return GetTarget(attackersInRange);
        }
    }
}