using System;
using Models;
using UnityEngine;
using Zenject;

namespace Attackers
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AttackerFactory")]
    public class AttackerFactory : ScriptableObject
    {
        public int CountAttackers { get; private set; }
        
        [Inject] private IInstantiator _instantiator;
        [Inject] private Wallet _wallet;

        private const string AttackerLayerName = "Attacker"; 

        public Attacker Get(Attacker attacker)
        {
            var newAttacker = _instantiator.InstantiatePrefab(attacker).GetComponent<Attacker>();
            newAttacker.Died += OnAttackerDied;
            newAttacker.gameObject.layer = LayerMask.NameToLayer(AttackerLayerName);
            CountAttackers++;
            return newAttacker;
        }

        private void OnAttackerDied(Attacker attacker)
        {
            _wallet.AddMoney(attacker.AttackerData.Reward);
            Reclaim(attacker);
        }

        public void Reclaim(Attacker attacker)
        {
            CountAttackers--;
            Destroy(attacker.gameObject);
        }
    }
}