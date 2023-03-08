using UnityEngine;

namespace Defender.Towers
{
    [CreateAssetMenu(fileName = "WarFactory", menuName = "ScriptableObjects/WarFactory")]
    public class WarFactory : ScriptableObject
    {
        [SerializeField] private Bullet _bullet;
        
        public Bullet GetBullet(Vector3 position)
        {
            return Instantiate(_bullet, position, Quaternion.identity);
        }
    }
}