using System.Collections.Generic;
using Attackers;

namespace Defender.Towers.TargetSelectors
{
    public interface ITargetSelector
    {
        /// <summary>
        /// Invoke when a new attacker appears in the range
        /// </summary>
        /// <param name="currentTarget">current target</param>
        /// <param name="newAttacker">new attacker in range</param>
        /// <returns></returns>
        Attacker NewAttackerInRange(Attacker currentTarget, Attacker newAttacker);

        /// <summary>
        /// Invoke when attacker in range changes the health
        /// </summary>
        /// <param name="currentTarget">current target</param>
        /// <param name="changedHealth">attacker which changed health</param>
        /// <param name="attackersInRange">collection of attackers in range</param>
        /// <returns></returns>
        Attacker AttackerHealthChanged(Attacker currentTarget, Attacker changedHealth, ICollection<Attacker> attackersInRange);

        /// <summary>
        /// Get description of the selector
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Get the target
        /// </summary>
        /// <param name="attackersInRange">collection of attackers in range</param>
        /// <returns>target</returns>
        Attacker GetTarget(ICollection<Attacker> attackersInRange);
    }
}