using Data.Towers;
using UnityEngine;

namespace Defender.Towers
{
    public enum PlacementTowerState
    {
        Available,
        Unavailable
    }

    public class Tower : MonoBehaviour
    {
        public PlacementTowerState CurrentState { get; private set; }

        [SerializeField] private TowerData _towerData;

        private Renderer _renderer;
        private readonly Color _availablePlaceColor = Color.green;
        private readonly Color _unavailablePlaceColor = Color.red;
        private readonly Color _normalColor = Color.white;
        
        public TowerData TowerData => _towerData;

        private void Awake()
        {
            _renderer = GetComponentInChildren<Renderer>();
        }

        public void HidePlacementState()
        {
            _renderer.material.color = _normalColor;
        }

        public void SetState(PlacementTowerState newState)
        {
            CurrentState = newState;

            switch (newState)
            {
                case PlacementTowerState.Available:
                    _renderer.material.color = _availablePlaceColor;
                    break;
                case PlacementTowerState.Unavailable:
                    _renderer.material.color = _unavailablePlaceColor;
                    break;
            }
        }
    }
}