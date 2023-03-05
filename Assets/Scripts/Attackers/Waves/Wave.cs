using System;
using UnityEngine;

namespace Attackers.Waves
{
    [Serializable]
    public class Wave
    {
        [SerializeField] private Attacker _attacker;
        [Range(1, 1000)] [SerializeField] private int _countAttackers;
        [Range(0.01f, 5f)] [SerializeField] private float _delayBetweenSpawn;

        public Attacker Attacker => _attacker;
        public int CountAttackers => _countAttackers;
        public float DelayBetweenSpawn => _delayBetweenSpawn;
    }
}