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
            _currentWaveIndex++;
            _currentWave = _waves[_currentWaveIndex];
            StartCoroutine(SpawnCoroutine());
        }

        private IEnumerator SpawnCoroutine()
        {
            foreach (var attackerPrefab in _currentWave.Attackers.Keys)       
            {
                for (int i = 0; i < _currentWave.Attackers[attackerPrefab]; i++)
                {
                    var attacker = _attackerFactory.Get(attackerPrefab);
                    attacker.transform.position = transform.position;
                    yield return new WaitForSeconds(_currentWave.DelayBetweenSpawn);
                }
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