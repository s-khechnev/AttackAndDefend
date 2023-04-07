using Attackers.Waves;
using Defender.Towers.Base;
using Models;
using UnityEngine;
using Zenject;

namespace Defender.Towers.Farming
{
    public class FarmingTower : BaseTower
    {
        [SerializeField] private FarmingTowerData _towerData;

        private FarmingTowerView _towerView;
        private float _elapsedTimeFromShoot;

        public override BaseTowerData BaseTowerData => _towerData;
        public override ITowerView TowerView => _towerView;

        private Wallet _wallet;

        [Inject]
        private void Construct(Wallet wallet)
        {
            _wallet = wallet;
        }

        private void Awake()
        {
            _towerData = Instantiate(_towerData);

            _towerView = GetComponent<FarmingTowerView>();
        }

        private void Update()
        {
            if (Spawner.WaveState == WaveState.Pause) return;

            _elapsedTimeFromShoot += Time.deltaTime;

            if (_elapsedTimeFromShoot >= _towerData.Cooldown.Value)
            {
                Shoot();
                _elapsedTimeFromShoot = 0;
            }
        }

        private void Shoot()
        {
            _wallet.AddMoney((int)_towerData.MoneyPerCooldown.Value);
        }
    }
}