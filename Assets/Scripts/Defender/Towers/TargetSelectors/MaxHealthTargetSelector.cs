using System;
using System.Collections.Generic;
using System.Linq;
using Attackers;

namespace Defender.Towers.TargetSelectors
{
    public class MaxHealthTargetSelector : ITargetSelector
    {
        public Func<Attacker, Attacker, Attacker> NewAttackerInRange => OnNewAttackerInRange;

        public Func<Attacker, Attacker, ICollection<Attacker>, Attacker> AttackerHealthChanged =>
            OnAttackerHealthChanged;

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

        private Attacker OnNewAttackerInRange(Attacker currentTarget, Attacker newAttacker)
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

        private Attacker OnAttackerHealthChanged(Attacker currentTarget, Attacker changedHealth,
            ICollection<Attacker> attackersInRange)
        {
            return GetTarget(attackersInRange);
        }
    }
}