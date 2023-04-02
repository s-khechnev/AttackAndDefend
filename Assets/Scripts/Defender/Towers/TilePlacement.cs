using UnityEngine;

namespace Defender.Towers
{
    public enum PlacementTileState
    {
        Filled,
        Empty,
    }

    [RequireComponent(typeof(Renderer))]
    public class TilePlacement : MonoBehaviour
    {
        public PlacementTileState CurrentState { get; private set; }

        private readonly Color _filledColor = new Color(1f, 0.34f, 0.27f);
        private readonly Color _emptyColor = new Color(0.38f, 1f, 0.27f);
        private readonly Color _normalColor = Color.white;
        
        private Color _realColor;
        private Renderer _tileRenderer;

        public Vector3 CenterPosition { get; private set; }

        private void Awake()
        {
            _tileRenderer = GetComponent<Renderer>();

            InitTileCenter();

            SetState(PlacementTileState.Empty);
        }

        private void InitTileCenter()
        {
            var center = _tileRenderer.bounds.center;
            center.y = 0;
            CenterPosition = center;
        }

        public void ShowState()
        {
            _tileRenderer.material.color = _realColor;
        }

        public void HideState()
        {
            _tileRenderer.material.color = _normalColor;
        }

        public void SetState(PlacementTileState newState)
        {
            CurrentState = newState;

            _realColor = newState switch
            {
                PlacementTileState.Filled => _filledColor,
                PlacementTileState.Empty => _emptyColor,
                _ => _realColor
            };
        }
    }
}