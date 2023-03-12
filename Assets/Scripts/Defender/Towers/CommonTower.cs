using Attackers;

namespace Defender.Towers
{
    public class CommonTower : Tower
    {
        protected override void Shoot(Attacker target)
        {
            var bullet = WarFactory.GetBullet(_launchPoint.position);
            bullet.Launch(target);
        }
    }
}