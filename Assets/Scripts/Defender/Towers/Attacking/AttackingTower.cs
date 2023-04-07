using Attackers;
using Defender.Towers.Base;
using Defender.Towers.Factories;
using UnityEngine;
using Zenject;

namespace Defender.Towers.Attacking
{
    public class AttackingTower : BaseTower
    {
        [SerializeField] private AttackingTowerData _towerData;
        [SerializeField] protected Transform _launchPoint;

        private float _elapsedTimeFromShoot;
        private IWarFactory _warFactory;

        private AttackingTowerView _towerView;

        public TargetFinder TargetFinder { get; private set; }

        public override BaseTowerData BaseTowerData => _towerData;
        public override ITowerView TowerView => _towerView;

        [Inject]
        private void Construct(IWarFactory warFactory)
        {
            _warFactory = warFactory;
        }

        private void Awake()
        {
            _towerData = Instantiate(_towerData);
            _towerData.Range.ValueChanged += OnRangeValueChanged;

            _towerView = GetComponent<AttackingTowerView>();

            TargetFinder = GetComponentInChildren<TargetFinder>();
            _elapsedTimeFromShoot = _towerData.Cooldown.Value;
        }

        private void Update()
        {
            _elapsedTimeFromShoot += Time.deltaTime;

            if (TargetFinder.Target == null) return;

            _towerView.LookAt(TargetFinder.Target.transform);

            if (_elapsedTimeFromShoot >= _towerData.Cooldown.Value)
            {
                Shoot(TargetFinder.Target);
                _elapsedTimeFromShoot = 0;
            }
        }

        private void OnDisable()
        {
            OnRangeValueChanged(_towerData.Range.Value);
        }

        protected virtual void Shoot(Attacker target)
        {
            var bullet = _warFactory.GetBullet(_towerData.Bullet, _launchPoint.position);
            bullet.Launch(target, (int)_towerData.Damage.Value);
        }

        private void OnRangeValueChanged(float newRange)
        {
            _towerView.SetRange(newRange);
            TargetFinder.SetRange(newRange);
        }
    }
}