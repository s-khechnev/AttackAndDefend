using System;
using Attackers;
using Data.Towers;
using UnityEngine;
using Zenject;

namespace Defender.Towers
{
    public abstract class Tower : MonoBehaviour
    {
        public Action<TowerData> TowerTapped;

        [Inject] protected TowerFactory TowerFactory;
        [Inject] protected WarFactory WarFactory;

        [SerializeField] protected Transform _launchPoint;
        [SerializeField] private TowerData _towerData;
        [SerializeField] private Transform _pivot;

        private float _elapsedTimeFromShoot;
        private TargetFinder _targetFinder;

        public TowerData TowerData => _towerData;

        protected abstract void Shoot(Attacker target);

        private void Awake()
        {
            _towerData = Instantiate(_towerData);

            _targetFinder = GetComponentInChildren<TargetFinder>();

            _elapsedTimeFromShoot = _towerData.Cooldown.Value;
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
                TowerTapped?.Invoke(_towerData);
        }
    }
}