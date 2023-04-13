using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Attackers.Waves
{
    public enum WaveState
    {
        InProgress,
        Pause
    }

    /// <summary>
    /// The component responsible for spawn of the attacker
    /// </summary>
    public class Spawner : MonoBehaviour
    {
        public event Action WaveEnded;
        public event Action AllWavesEnded;

        [SerializeField] private List<Wave> _waves;
        private int _currentWaveIndex;

        private IAttackerFactory _attackerFactory;

        public static WaveState WaveState { get; private set; }

        public bool IsWavesEnded => _currentWaveIndex + 1 == _waves.Count;

        [Inject]
        private void Construct(IAttackerFactory attackerFactory)
        {
            _attackerFactory = attackerFactory;
        }

        private void Awake()
        {
            WaveState = WaveState.Pause;
            _currentWaveIndex = -1;
        }

        /// <summary>
        /// Start the next wave
        /// </summary>
        public void StartNextWave()
        {
            WaveState = WaveState.InProgress;

            _currentWaveIndex++;
            StartCoroutine(SpawnCoroutine(_waves[_currentWaveIndex]));
        }

        /// <summary>
        /// Coroutine to spawn attackers
        /// </summary>
        /// <param name="wave">wave to spawn</param>
        /// <returns></returns>
        private IEnumerator SpawnCoroutine(Wave wave)
        {
            foreach (var attackerPrefab in wave.Attackers.Keys)
            {
                for (var i = 0; i < wave.Attackers[attackerPrefab]; i++)
                {
                    _attackerFactory.Create(attackerPrefab, transform.position);
                    yield return new WaitForSeconds(wave.DelayBetweenSpawn);
                }
            }

            yield return new WaitUntil(() => _attackerFactory.CountAttackers == 0);

            WaveState = WaveState.Pause;

            if (IsWavesEnded)
                AllWavesEnded?.Invoke();
            else
                WaveEnded?.Invoke();
        }
    }
}