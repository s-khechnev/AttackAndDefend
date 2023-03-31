using System;
using System.Collections.Generic;
using System.Linq;
using Attackers;

namespace Defender.Towers.TargetSelectors
{
    public class NearestTargetSelector : ITargetSelector
    {
        public Func<Attacker, Attacker, Attacker> NewAttackerInRange => OnNewAttackerInRange;

        public Func<Attacker, Attacker, ICollection<Attacker>, Attacker> AttackerHealthChanged =>
            OnAttackerHealthChanged;

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

        private Attacker OnNewAttackerInRange(Attacker currentTarget, Attacker newAttacker)
        {
            if (currentTarget == null || newAttacker.DistanceToCastle < currentTarget.DistanceToCastle)
                return newAttacker;

            return currentTarget;
        }

        private Attacker OnAttackerHealthChanged(Attacker currentTarget, Attacker changedHealth,
            ICollection<Attacker> inRange)
        {
            return currentTarget;
        }
    }
}