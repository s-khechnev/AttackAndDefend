using Attackers;
using UnityEngine;
using Zenject;

namespace Defender.Towers
{
    [SelectionBase, RequireComponent(typeof(BoxCollider))]
    public abstract class Tower : MonoBehaviour
    {
        [Inject] protected WarFactory WarFactory;

        [SerializeField] protected Transform _launchPoint;
        [SerializeField] private TowerData _towerData;

        private float _elapsedTimeFromShoot;

        public TowerData TowerData => _towerData;
        public TowerView TowerView { get; private set; }
        public TargetFinder TargetFinder { get; private set; }

        protected abstract void Shoot(Attacker target);

        private void Awake()
        {
            _towerData = Instantiate(_towerData);
            _towerData.Range.ValueChanged += OnRangeValueChanged;

            TargetFinder = GetComponentInChildren<TargetFinder>();

            TowerView = GetComponent<TowerView>();

            _elapsedTimeFromShoot = _towerData.Cooldown.Value;
        }

        private void Start()
        {
            OnRangeValueChanged(_towerData.Range.Value);
        }

        private void Update()
        {
            _elapsedTimeFromShoot += Time.deltaTime;

            if (TargetFinder.Target == null) return;
            
            TowerView.LookAt(TargetFinder.Target.transform);
            
            if (_elapsedTimeFromShoot >= _towerData.Cooldown.Value)
            {
                Shoot(TargetFinder.Target);
                _elapsedTimeFromShoot = 0;
            }
        }

        private void OnRangeValueChanged(float newRange)
        {
            TargetFinder.InitRange(newRange);
        }
    }
}