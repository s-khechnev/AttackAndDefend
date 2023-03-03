using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Attacker.Waves
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private List<Wave> _waves;

        [Inject] private AttackerFactory _attackerFactory;
        
        private Wave _currentWave;
        private int _currentWaveIndex;
        private int _countSpawned;

        private float _elapsedTime;

        private void Start()
        {
            _currentWaveIndex = 0;
            SetWave(_currentWaveIndex);
        }

        private void Update()
        {
            if (_currentWave == null)
                return;

            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= _currentWave.DelayBetweenSpawn)
            {
                _elapsedTime = 0;
                
                var attacker = _attackerFactory.Get(_currentWave.AttackerData);
                attacker.transform.position = transform.position;
                
                _countSpawned += 1;
            }

            if (_currentWave.CountAttackers <= _countSpawned)
            {
                if (_currentWaveIndex + 1 < _waves.Count)
                {
                    SetWave(_currentWaveIndex + 1);
                }
                else
                {
                    _currentWave = null;
                }
            }
        }

        private void SetWave(int index)
        {
            _currentWaveIndex = index;
            _currentWave = _waves[index];
            
            _elapsedTime = 0;
            _countSpawned = 0;
        }
    }
}