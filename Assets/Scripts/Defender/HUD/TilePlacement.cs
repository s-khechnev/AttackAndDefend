using UnityEngine;

namespace Defender.HUD
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

        public Vector3 CenterPosition => transform.GetChild(0).transform.position;

        private void Awake()
        {
            _tileRenderer = GetComponent<Renderer>();

            SetState(PlacementTileState.Empty);
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

            switch (newState)
            {
                case PlacementTileState.Filled:
                    _realColor = _filledColor;
                    break;
                case PlacementTileState.Empty:
                    _realColor = _emptyColor;
                    break;
            }
        }
    }
}