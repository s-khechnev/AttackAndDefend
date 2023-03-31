using System;
using System.Collections.Generic;
using System.Linq;
using Attackers;

namespace Defender.Towers.TargetSelectors
{
    public class MinHealthTargetSelector : ITargetSelector
    {
        public Func<Attacker, Attacker, Attacker> NewAttackerInRange => OnNewAttackerInRange;

        public Func<Attacker, Attacker, ICollection<Attacker>, Attacker> AttackerHealthChanged =>
            OnAttackerHealthChanged;

        public string Description => "Min health";

        public Attacker GetTarget(ICollection<Attacker> attackersInRange)
        {
            if (attackersInRange.Count == 0)
                return null;

            var minHealthAttacker = attackersInRange.First();
            foreach (var attacker in attackersInRange)
            {
                if (attacker.Health < minHealthAttacker.Health)
                {
                    minHealthAttacker = attacker;
                }
                else if (attacker.Health == minHealthAttacker.Health &&
                         attacker.DistanceToCastle < minHealthAttacker.DistanceToCastle)
                {
                    minHealthAttacker = attacker;
                }
            }

            return minHealthAttacker;
        }

        private Attacker OnNewAttackerInRange(Attacker currentTarget, Attacker newAttacker)
        {
            if (currentTarget == null)
                return newAttacker;

            if (currentTarget.Health > newAttacker.Health)
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
            if (currentTarget == null)
                return changedHealth;

            if (currentTarget.Health > changedHealth.Health)
                return changedHealth;

            if (currentTarget.Health == changedHealth.Health &&
                currentTarget.DistanceToCastle > changedHealth.DistanceToCastle)
            {
                return changedHealth;
            }

            return currentTarget;
        }
    }
}