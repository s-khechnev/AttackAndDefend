using System;
using System.Collections;
using System.Collections.Generic;
using Defender.HUD;
using UnityEngine;
using Zenject;

namespace Attackers.Waves
{
    public class Spawner : MonoBehaviour
    {
        public event Action WaveEnded;
        public event Action AllWavesEnded;

        [SerializeField] private List<Wave> _waves;
        [SerializeField] private HUDManager _hudManager;

        [Inject] private AttackerFactory _attackerFactory;

        private Wave _currentWave;
        private int _currentWaveIndex;
        private int _countSpawned;

        private void Awake()
        {
            _currentWaveIndex = -1;
            _hudManager.NextWaveTapped += OnNextWaveTapped;
        }

        private void OnNextWaveTapped()
        {
            StartNextWave();
        }

        private void StartNextWave()
        {
            _countSpawned = 0;
            _currentWaveIndex++;
            _currentWave = _waves[_currentWaveIndex];
            StartCoroutine(SpawnCoroutine());
        }

        private IEnumerator SpawnCoroutine()
        {
            while (_currentWave.CountAttackers > _countSpawned)
            {
                var attacker = _attackerFactory.Get(_currentWave.AttackerData);
                attacker.transform.position = transform.position;
                _countSpawned++;
                yield return new WaitForSeconds(_currentWave.DelayBetweenSpawn);
            }

            yield return new WaitUntil(() => _attackerFactory.CountAttackers == 0);

            if (_currentWaveIndex + 1 == _waves.Count)
            {
                AllWavesEnded?.Invoke();
            }
            else
            {
                WaveEnded?.Invoke();
            }
        }
    }
}