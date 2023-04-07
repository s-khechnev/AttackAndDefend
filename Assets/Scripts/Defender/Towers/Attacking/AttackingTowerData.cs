using System.Collections.Generic;
using Defender.Towers.Base;
using UnityEngine;

namespace Defender.Towers.Attacking
{
    [CreateAssetMenu(fileName = "AttackingTowerData", menuName = "ScriptableObjects/Tower/AttackingTowerData")]
    public class AttackingTowerData : BaseTowerData
    {
        [SerializeField] private Attribute _attackRange;
        [SerializeField] private Attribute _damage;

        [SerializeField] private Bullet _bullet;

        public Attribute Range => _attackRange;
        public Attribute Damage => _damage;
        public Bullet Bullet => _bullet;

        private List<Attribute> _attributes;

        public override List<Attribute> Attributes => _attributes;

        private void Awake()
        {
            _attributes = new List<Attribute> { _cooldown, _attackRange, _damage };
        }
    }
}