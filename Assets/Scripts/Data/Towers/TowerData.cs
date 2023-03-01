using UnityEngine;

namespace Data.Towers
{
    public enum TowerType
    {
        DefaultTower,
    }
    
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Tower")]
    public class TowerData : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private GameObject _towerPrefab;
        [SerializeField] private TowerType _towerType;
        [SerializeField] private int _cost;

        public string Name => _name;
        public GameObject TowerPrefab => _towerPrefab;
        public TowerType TowerType => _towerType;
        public int Cost => _cost;
    }
}