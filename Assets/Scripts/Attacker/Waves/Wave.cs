using System;
using Data.Attackers;
using UnityEngine;

namespace Attacker.Waves
{
    [Serializable]
    public class Wave
    {
        [SerializeField] private AttackerData _attackerData;

        [Range(1, 1000)] [SerializeField] private int _countAttackers;

        [Range(0.01f, 5f)] [SerializeField] private float _delayBetweenSpawn;

        public AttackerData AttackerData => _attackerData;
        public int CountAttackers => _countAttackers;
        public float DelayBetweenSpawn => _delayBetweenSpawn;
    }
}