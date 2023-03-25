using System.Collections.Generic;
using Attackers;
using UnityEngine;

namespace Defender.Towers
{
    [RequireComponent(typeof(SphereCollider))]
    public abstract class TargetFinder : MonoBehaviour
    {
        public Attacker Target
        {
            get => _target;
            protected set
            {
                if (value != null)
                    value.Died += OnTargetDied;
                if (_target != null)
                    _target.Died -= OnTargetDied;

                _target = value;
            }
        }

        private Attacker _target;
        protected List<Attacker> AttackersInRange;

        private SphereCollider _rangeCollider;
        private float _attackRange;
        
        protected abstract void FindTarget();
        protected abstract void OnNewAttackerInRange(Attacker newAttacker);

        private void Awake()
        {
            _rangeCollider = GetComponent<SphereCollider>();
            AttackersInRange = new();
        }
        
        public void InitRange(float range)
        {
            _attackRange = range;
            _rangeCollider.radius = range;
        }

        private void OnTargetDied(Attacker target)
        {
            AttackersInRange.Remove(target);
            FindTarget();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Attacker newAttacker))
            {
                AttackersInRange.Add(newAttacker);
                OnNewAttackerInRange(newAttacker);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Attacker attacker))
            {
                AttackersInRange.Remove(attacker);

                if (Target == attacker)
                    FindTarget();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _attackRange);
        }
    }
}