using UnityEngine;

namespace Defender.Towers
{
    public enum PlacementTowerState
    {
        Available,
        Unavailable,
    }

    public class TowerGhost
    {
        private readonly Renderer[] _renderers;
        private readonly Color _availablePlaceColor = Color.green;
        private readonly Color _unavailablePlaceColor = Color.red;
        private readonly Color _normalColor = Color.white;

        private PlacementTowerState _currentState;

        public TowerGhost(Renderer[] renderers)
        {
            _renderers = renderers;
        }

        public void SetState(PlacementTowerState newState)
        {
            _currentState = newState;
            
            switch (newState)
            {
                case PlacementTowerState.Available:
                    SetColor(_availablePlaceColor);
                    break;
                case PlacementTowerState.Unavailable:
                    SetColor(_unavailablePlaceColor);
                    break;
            }
        }

        private void SetColor(Color color)
        {
            foreach (var rend in _renderers)
            {
                rend.material.color = color;
            }
        }

        public void Hide()
        {
            SetColor(_normalColor);
        }

        public void Show()
        {
            SetState(_currentState);
        }
    }
}