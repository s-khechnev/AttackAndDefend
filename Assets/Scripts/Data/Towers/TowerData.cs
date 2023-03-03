using UnityEngine;

namespace Data.Towers
{
    public enum TowerType
    {
        Common,
    }
    
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Tower")]
    public class TowerData : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private GameObject _towerPrefab;
        [SerializeField] private TowerType _type;
        [SerializeField] private int _cost;

        public string Name => _name;
        public GameObject TowerPrefab => _towerPrefab;
        public TowerType Type => _type;
        public int Cost => _cost;
    }
}