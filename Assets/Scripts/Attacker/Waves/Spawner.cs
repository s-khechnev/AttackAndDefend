using System.Collections.Generic;
using UnityEngine;

namespace Attacker.Waves
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private List<Wave> _waves;

        private Wave _currentWave;
        private int _currentWaveIndex;

        private float _elapsedTime;

        private int _countSpawned;

        private void Start()
        {
            _currentWaveIndex = 0;
            SetWave(_currentWaveIndex);

            _elapsedTime = 0;

            _countSpawned = 0;
        }

        private void Update()
        {
            if (_currentWave == null)
                return;

            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= _currentWave.DelayBetweenSpawn)
            {
                _elapsedTime = 0;

                Instantiate(_currentWave.AttackerPrefab, transform.position, Quaternion.identity);
                _countSpawned += 1;
            }

            if (_currentWave.CountAttackers <= _countSpawned)
            {
                _currentWave = null;
            }
        }

        private void SetWave(int index)
        {
            _currentWave = _waves[index];
        }
    }
}