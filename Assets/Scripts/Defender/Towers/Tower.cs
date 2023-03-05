using Data.Towers;
using UnityEngine;
using Zenject;

namespace Defender.Towers
{
    public abstract class Tower : MonoBehaviour
    {
        [SerializeField] private TowerData _towerData;
        
        [Inject] protected TowerFactory Factory;

        public TowerData TowerData => _towerData;
    }
}