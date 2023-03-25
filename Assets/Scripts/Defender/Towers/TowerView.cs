using System;
using UnityEngine;

namespace Defender.Towers
{
    [RequireComponent(typeof(Tower), typeof(LineRenderer))]
    public class TowerView : MonoBehaviour
    {
        public event Action<TowerView> TowerTapped;

        [SerializeField] private Material _circleRangeMaterial;

        private TowerGhost _towerGhost;
        private RangeViewer _rangeViewer;

        public Tower Tower { get; private set; }

        private void Awake()
        {
            Tower = GetComponent<Tower>();
            _towerGhost = new TowerGhost(GetComponentsInChildren<Renderer>());
            _rangeViewer = new RangeViewer(GetComponent<LineRenderer>(), _circleRangeMaterial);

            Tower.TowerData.Range.ValueChanged += OnRangeValueChanged;
        }

        private void Start()
        {
            OnRangeValueChanged(Tower.TowerData.Range.Value);
        }

        private void OnRangeValueChanged(float newRange)
        {
            _rangeViewer.DrawCircle(newRange);
        }

        public void SetPlacementState(PlacementTowerState state)
        {
            _towerGhost.SetState(state);
        }

        public void HideState()
        {
            _towerGhost.Hide();
            _rangeViewer.Hide();
        }

        public void ShowState()
        {
            _towerGhost.Show();
            _rangeViewer.Show();
        }

        private void OnMouseDown()
        {
            if (isActiveAndEnabled)
                TowerTapped?.Invoke(this);
        }
    }
}