using System;
using Attackers;
using UnityEngine;
using Zenject;

namespace Defender.Towers
{
    [SelectionBase, RequireComponent(typeof(BoxCollider))]
    public abstract class Tower : MonoBehaviour
    {
        public Action<TowerData, RangeViewer> TowerTapped;

        [Inject] protected TowerFactory TowerFactory;
        [Inject] protected WarFactory WarFactory;

        [SerializeField] protected Transform _launchPoint;
        [SerializeField] private TowerData _towerData;
        [SerializeField] private Transform _pivot;

        private float _elapsedTimeFromShoot;
        private TargetFinder _targetFinder;
        private RangeViewer _rangeViewer;

        public TowerData TowerData => _towerData;

        protected abstract void Shoot(Attacker target);

        private void Awake()
        {
            _towerData = Instantiate(_towerData);
            _towerData.Range.ValueChanged += OnRangeValueChanged;

            _targetFinder = GetComponentInChildren<TargetFinder>();
            _rangeViewer = GetComponentInChildren<RangeViewer>();

            _elapsedTimeFromShoot = _towerData.Cooldown.Value;
        }

        private void Start()
        {
            OnRangeValueChanged(_towerData.Range.Value);
        }

        private void Update()
        {
            _elapsedTimeFromShoot += Time.deltaTime;

            if (_targetFinder.Target != null)
            {
                _pivot.LookAt(_targetFinder.Target.transform);

                if (_elapsedTimeFromShoot >= _towerData.Cooldown.Value)
                {
                    Shoot(_targetFinder.Target);
                    _elapsedTimeFromShoot = 0;
                }
            }
        }

        private void OnMouseDown()
        {
            if (isActiveAndEnabled)
                TowerTapped?.Invoke(_towerData, _rangeViewer);
        }

        private void OnRangeValueChanged(float newRange)
        {
            _targetFinder.InitRange(newRange);
            _rangeViewer.DrawCircle(newRange);
        }
    }
}