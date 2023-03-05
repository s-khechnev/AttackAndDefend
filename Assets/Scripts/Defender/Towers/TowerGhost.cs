using UnityEngine;

namespace Defender.Towers
{
    public enum PlacementTowerState
    {
        Available,
        Unavailable
    }

    public class TowerGhost : MonoBehaviour
    {
        public Tower Tower { get; set; }

        private Renderer _renderer;
        private readonly Color _availablePlaceColor = Color.green;
        private readonly Color _unavailablePlaceColor = Color.red;
        private readonly Color _normalColor = Color.white;

        private void Awake()
        {
            _renderer = GetComponentInChildren<Renderer>();
        }

        public void SetState(PlacementTowerState newState)
        {
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

        private void OnDestroy()
        {
            _renderer.material.color = _normalColor;
        }
    }
}