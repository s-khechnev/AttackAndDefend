using UnityEngine;

namespace Defender.Towers.Factories
{
    public interface IWarFactory
    {
        Bullet GetBullet(Bullet bullet, Vector3 position);
    }
}