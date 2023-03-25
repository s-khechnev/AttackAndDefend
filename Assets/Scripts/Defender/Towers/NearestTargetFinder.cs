using Attackers;

namespace Defender.Towers
{
    public class NearestTargetFinder : TargetFinder
    {
        protected override void FindTarget()
        {
            switch (AttackersInRange.Count)
            {
                case 0:
                    Target = null;
                    return;
                case 1:
                    Target = AttackersInRange[0];
                    return;
            }

            var nearestToCastle = AttackersInRange[0];

            foreach (var attacker in AttackersInRange)
            {
                if (nearestToCastle.DistanceToCastle > attacker.DistanceToCastle)
                    nearestToCastle = attacker;
            }

            Target = nearestToCastle;
        }

        protected override void OnNewAttackerInRange(Attacker newAttacker)
        {
            if (Target == null || newAttacker.DistanceToCastle < Target.DistanceToCastle)
                Target = newAttacker;
        }
    }
}