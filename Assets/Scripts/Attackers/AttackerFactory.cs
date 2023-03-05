using UnityEngine;
using Zenject;

namespace Attackers
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AttackerFactory")]
    public class AttackerFactory : ScriptableObject
    {
        [Inject] private IInstantiator _instantiator;

        public int CountAttackers { get; private set; }

        public Attacker Get(Attacker attacker)
        {
            var newAttacker = _instantiator.InstantiatePrefab(attacker).GetComponent<Attacker>();
            CountAttackers++;
            return newAttacker;
        }

        public void Reclaim(Attacker attacker)
        {
            CountAttackers--;
            Destroy(attacker.gameObject);
        }
    }
}