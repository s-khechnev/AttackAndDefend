using Data.Attackers;

namespace Attackers
{
    public class CommonAttacker : Attackers.Attacker
    {
        public override AttackerType Type => AttackerType.Common;
    }
}