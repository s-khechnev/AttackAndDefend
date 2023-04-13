using System;
using System.Collections.Generic;
using UnityEngine;

namespace Attackers.Waves
{
    [Serializable]
    public class Wave
    {
        /// <summary>
        /// The dictionary in which the key is prefab of attacker, value is count of this attacker
        /// </summary>
        [SerializeField] private SerializableDictionary<Attacker, int> _attackers;
        
        /// <summary>
        /// Delay between spawn of attackers 
        /// </summary>
        [SerializeField, Range(0.01f, 5f)] private float _delayBetweenSpawn;

        /// <summary>
        /// Returns dictionary in which the key is prefab of attacker, value is count of this attacker
        /// </summary>
        public IDictionary<Attacker, int> Attackers => _attackers;
        
        /// <summary>
        /// Returns delay between spawn of attackers 
        /// </summary>
        public float DelayBetweenSpawn => _delayBetweenSpawn;
    }
}