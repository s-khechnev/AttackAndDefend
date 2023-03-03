using Data.Attackers;
using UnityEngine;
using Zenject;

namespace Attacker
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AttackerFactory")]
    public class AttackerFactory : ScriptableObject
    {
        [SerializeField] private AttackerData _commonAttackerData;

        [Inject] private IInstantiator _instantiator;
        
        public Attacker Get(AttackerData attackerData)
        {
            switch (attackerData.Type)
            {
                case AttackerType.Common:
                    return _instantiator.InstantiatePrefab(_commonAttackerData.Prefab).GetComponent<Attacker>();
            }

            return null;
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