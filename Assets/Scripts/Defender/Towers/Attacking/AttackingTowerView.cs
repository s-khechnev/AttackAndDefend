using Defender.Towers.Base;
using UnityEngine;

namespace Defender.Towers.Attacking
{
    [RequireComponent(typeof(AttackingTower), typeof(LineRenderer))]
    public class AttackingTowerView : MonoBehaviour, ITowerView
    {
        [SerializeField] private Material _circleRangeMaterial;

        [SerializeField] private Transform _pivot;

        private TowerGhost _towerGhost;
        private RangeViewer _rangeViewer;

        private void Awake()
        {
            _towerGhost = new TowerGhost(GetComponentsInChildren<Renderer>());
            _rangeViewer = new RangeViewer(GetComponent<LineRenderer>(), _circleRangeMaterial);
        }

        public void SetRange(float newRange)
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

        public void LookAt(Transform targetTransform)
        {
            _pivot.LookAt(targetTransform);
        }
    }
}