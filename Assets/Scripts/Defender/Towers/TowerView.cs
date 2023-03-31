using System;
using Defender.HUD;
using UnityEngine;

namespace Defender.Towers
{
    [RequireComponent(typeof(Tower), typeof(LineRenderer))]
    public class TowerView : MonoBehaviour
    {
        public event Action<Tower> TowerTapped;

        [SerializeField] private Material _circleRangeMaterial;

        [SerializeField] private Transform _pivot;

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

        private void OnMouseDown()
        {
            if (!isActiveAndEnabled || DefenderGUIManager.GameState == DefenderGameState.Building)
                return;
            
            ShowState();
            TowerTapped?.Invoke(Tower);
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

        private void ShowState()
        {
            _towerGhost.Show();
            _rangeViewer.Show();
        }

        public void LookAt(Transform targetTransform)
        {
            _pivot.LookAt(targetTransform);
        }
    }
}