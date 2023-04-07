using System.Collections.Generic;
using UnityEngine;

namespace Defender.Towers.Base
{
    public abstract class BaseTowerData : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private int _cost;
        [SerializeField] private int _costToRelocate;

        [SerializeField] protected Attribute _cooldown;

        public string Name => _name;
        public int Cost => _cost;
        public int CostToRelocate => _costToRelocate;
        public Attribute Cooldown => _cooldown;

        public abstract List<Attribute> Attributes { get; }
    }
}