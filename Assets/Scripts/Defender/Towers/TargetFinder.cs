using System.Collections.Generic;
using Attackers;
using Unity.VisualScripting;
using UnityEngine;

namespace Defender.Towers
{
    [RequireComponent(typeof(SphereCollider))]
    public class TargetFinder : MonoBehaviour
    {
        public Attacker Target { get; private set; }
        
        private SphereCollider _rangeCollider;
        private float _attackRange;
        
        private Queue<Attacker> _attackersQueue;
        
        private void Awake()
        {
            _attackRange = transform.parent.GetComponent<Tower>().TowerData.AttackRange;
            
            _rangeCollider = GetComponent<SphereCollider>();
            _rangeCollider.radius = _attackRange;

            _attackersQueue = new();
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
                else
                    _attackersQueue.Dequeue();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _attackRange);
        }
    }
}