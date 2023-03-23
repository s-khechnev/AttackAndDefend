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

        private RangeViewer _rangeViewer;
        
        private Renderer[] _renderers;
        private readonly Color _availablePlaceColor = Color.green;
        private readonly Color _unavailablePlaceColor = Color.red;
        private readonly Color _normalColor = Color.white;

        private void Awake()
        {
            _renderers = GetComponentsInChildren<Renderer>();
            _rangeViewer = GetComponentInChildren<RangeViewer>();
        }

        private void Start()
        {
            _rangeViewer.DrawCircle(Tower.TowerData.Range.Value);
        }

        public void SetState(PlacementTowerState newState)
        {
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

        private void OnDestroy()
        {
            _rangeViewer.Hide();
            SetColor(_normalColor);
        }

        private void SetColor(Color color)
        {
            foreach (var rend in _renderers)
            {
                rend.material.color = color;
            }
        }
    }
}