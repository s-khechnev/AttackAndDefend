using Attackers;

namespace Defender.Towers
{
    public class CommonTower : Tower
    {
        protected override void Shoot(Attacker target)
        {
            var bullet = WarFactory.GetBullet(TowerData.Bullet, _launchPoint.position);
            bullet.Launch(target, TowerData.Damage.Value);
        }
    }
}