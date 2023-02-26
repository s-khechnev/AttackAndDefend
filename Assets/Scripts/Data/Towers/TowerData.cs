using Defender.Towers;
using UnityEngine;

namespace Data.Towers
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Tower")]
    public class TowerData : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Tower _tower;

        public string Name => _name;
        public Tower Tower => _tower;
    }
}