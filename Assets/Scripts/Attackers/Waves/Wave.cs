using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Attackers.Waves
{
    [Serializable]
    public class Wave
    {
        [SerializeField] private SerializableDictionary<Attacker, int> _attackers;
        [SerializeField, Range(0.01f, 5f)] private float _delayBetweenSpawn;

        public IDictionary<Attacker, int> Attackers => _attackers;
        public float DelayBetweenSpawn => _delayBetweenSpawn;
    }
}