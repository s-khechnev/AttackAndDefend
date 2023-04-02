using UnityEngine;

namespace Defender.Towers.Factories
{
    public class WarFactory : IWarFactory
    {
        public Bullet GetBullet(Bullet bullet, Vector3 position)
        {
            return Object.Instantiate(bullet, position, Quaternion.identity);
        }
    }
}