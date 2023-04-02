using Defender.Towers;
using UnityEngine;
using UnityEngine.UI;

namespace Defender.HUD
{
    public class BuildTowerButton : Button
    {
        [SerializeField] private Tower _tower;

        public Tower Tower => _tower;
        public TowerView TowerView { get; private set; }

        protected override void Awake()
        {
            TowerView = _tower.GetComponent<TowerView>();
        }
    }
}