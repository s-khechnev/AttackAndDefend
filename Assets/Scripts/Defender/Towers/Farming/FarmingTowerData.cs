using System.Collections.Generic;
using Defender.Towers.Base;
using UnityEngine;

namespace Defender.Towers.Farming
{
    [CreateAssetMenu(fileName = "FarmingTowerData", menuName = "ScriptableObjects/Tower/FarmingTowerData")]
    public class FarmingTowerData : BaseTowerData
    {
        [SerializeField] private Attribute _moneyPerCooldown;

        private List<Attribute> _attributes;

        public Attribute MoneyPerCooldown => _moneyPerCooldown;

        public override List<Attribute> Attributes => _attributes;

        private void Awake()
        {
            _attributes = new List<Attribute> { Cooldown, _moneyPerCooldown };
        }
    }
}