using System;
using UnityEngine;
using Zenject;

namespace Attackers
{
    public class AttackerFactory : IAttackerFactory
    {
        public event Action<Attacker> AttackerDied;

        public int CountAttackers { get; private set; }

        private const string AttackerLayerName = "Attacker";

        private readonly IInstantiator _instantiator;

        [Inject]
        public AttackerFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public Attacker Create(Attacker prefab, Vector3 position)
        {
            var attacker =
                _instantiator.InstantiatePrefabForComponent<Attacker>(prefab, position, Quaternion.identity,
                    null);

            attacker.gameObject.layer = LayerMask.NameToLayer(AttackerLayerName);
            attacker.Died += OnAttackerDied;

            CountAttackers++;

            return attacker;
        }

        public void Destroy(Attacker attackerToDestroy)
        {
            CountAttackers--;
            UnityEngine.Object.Destroy(attackerToDestroy.gameObject);
        }

        private void OnAttackerDied(Attacker died)
        {
            AttackerDied?.Invoke(died);
        }
    }
}