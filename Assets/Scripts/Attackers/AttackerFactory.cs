using Data.Attackers;
using UnityEngine;
using Zenject;

namespace Attackers
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AttackerFactory")]
    public class AttackerFactory : ScriptableObject
    {
        [SerializeField] private AttackerData _commonAttackerData;

        [Inject] private IInstantiator _instantiator;
        
        public int CountAttackers { get; private set; }

        public Attackers.Attacker Get(AttackerData attackerData)
        {
            Attackers.Attacker newAttacker = null;
            switch (attackerData.Type)
            {
                case AttackerType.Common:
                    newAttacker = _instantiator.InstantiatePrefab(_commonAttackerData.Prefab).GetComponent<Attackers.Attacker>();
                    break;
            }

            if (newAttacker != null)
                CountAttackers++;
            
            return newAttacker;
        }

        public void Reclaim(Attackers.Attacker attacker)
        {
            CountAttackers--;

            Destroy(attacker.gameObject);
        }

        public AttackerData GetAttackerData(AttackerType attackerType)
        {
            switch (attackerType)
            {
                case AttackerType.Common:
                    return _commonAttackerData;
            }

            return null;
        }
    }
}