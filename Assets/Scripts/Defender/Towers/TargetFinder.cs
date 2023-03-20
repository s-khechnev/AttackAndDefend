using System.Collections.Generic;
using Attackers;
using Data.Towers;
using Unity.VisualScripting;
using UnityEngine;

namespace Defender.Towers
{
    [RequireComponent(typeof(SphereCollider))]
    public class TargetFinder : MonoBehaviour
    {
        public Attacker Target { get; private set; }

        private SphereCollider _rangeCollider;
        private TowerData _towerData;
        private float _attackRange;

        private Queue<Attacker> _attackersQueue;

        private void Awake()
        {
            _towerData = transform.parent.GetComponent<Tower>().TowerData;
            _towerData.TowerRangeChanged += OnTowerRangeChanged;

            _rangeCollider = GetComponent<SphereCollider>();
            InitRange();

            _attackersQueue = new();
        }

        private void InitRange()
        {
            _attackRange = _towerData.Range.Value;
            _rangeCollider.radius = _attackRange;
        }

        private void OnTowerRangeChanged()
        {
            InitRange();
        }

        private void Update()
        {
            if (_attackersQueue.Count != 0 && (Target == null || Target.IsDestroyed()))
            {
                Target = _attackersQueue.Dequeue();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Attacker attacker))
            {
                _attackersQueue.Enqueue(attacker);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Attacker attacker))
            {
                if (Target == attacker)
                    Target = null;
                else if (_attackersQueue.Count != 0)
                    _attackersQueue.Dequeue();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _attackRange);
        }
    }
}