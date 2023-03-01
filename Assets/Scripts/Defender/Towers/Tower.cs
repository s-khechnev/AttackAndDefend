using Data.Towers;
using UnityEngine;
using Zenject;

namespace Defender.Towers
{
    public abstract class Tower : MonoBehaviour
    {
        [Inject] protected TowerFactory Factory;
        public abstract TowerType Type { get; }
        public TowerData TowerData => Factory.GetTowerData(Type);
    }
}