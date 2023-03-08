namespace Defender.Towers
{
    public class CommonTower : Tower
    {
        protected override void Shoot()
        {
            var bullet = WarFactory.GetBullet(_launchPoint.position);
            bullet.Launch(Target);
        }
    }
}