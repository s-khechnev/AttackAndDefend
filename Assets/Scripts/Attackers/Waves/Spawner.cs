using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Attackers.Waves
{
    public class Spawner : MonoBehaviour
    {
        public event Action WaveEnded;
        public event Action AllWavesEnded;
        
        [SerializeField] private List<Wave> _waves;
        
        private Wave _currentWave;
        private int _currentWaveIndex;

        private IAttackerFactory _attackerFactory;
        
        public bool IsWavesEnded => _currentWaveIndex + 1 == _waves.Count;

        [Inject]
        private void Construct(IAttackerFactory attackerFactory)
        {
            _attackerFactory = attackerFactory;
        }

        private void Awake()
        {
            _currentWaveIndex = -1;
        }

        public void StartNextWave()
        {
            _currentWaveIndex++;
            _currentWave = _waves[_currentWaveIndex];
            StartCoroutine(SpawnCoroutine());
        }

        private IEnumerator SpawnCoroutine()
        {
            foreach (var attackerPrefab in _currentWave.Attackers.Keys)
            {
                for (var i = 0; i < _currentWave.Attackers[attackerPrefab]; i++)
                {
                    _attackerFactory.Create(attackerPrefab, transform.position);
                    yield return new WaitForSeconds(_currentWave.DelayBetweenSpawn);
                }
            }

            yield return new WaitUntil(() => _attackerFactory.CountAttackers == 0);

            if (IsWavesEnded)
                AllWavesEnded?.Invoke();
            else
                WaveEnded?.Invoke();
        }
    }
}