using UnityEngine;

namespace Models
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Player")]
    public class PlayerInfo : ScriptableObject
    {
        [SerializeField] private string _name;

        public string Name => _name;
    }
}