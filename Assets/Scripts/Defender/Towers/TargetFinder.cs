using System.Collections.Generic;
using Attackers;
using Defender.Towers.TargetSelectors;
using Helpers;
using UnityEngine;

namespace Defender.Towers
{
    [RequireComponent(typeof(SphereCollider))]
    public class TargetFinder : MonoBehaviour
    {
        public Attacker Target { get; private set; }

        private List<Attacker> _attackersInRange;

        private LinkedListNode<ITargetSelector> _currentSelectorNode;
        private LinkedList<ITargetSelector> _targetSelectors;
        private ITargetSelector CurrentSelector => _currentSelectorNode.Value;

        private SphereCollider _rangeCollider;
        private float _attackRange;

        private void Awake()
        {
            _rangeCollider = GetComponent<SphereCollider>();
            _attackersInRange = new();
            InitSelector();
        }

        private void InitSelector()
        {
            _targetSelectors = new LinkedList<ITargetSelector>();
            _targetSelectors.AddLast(new NearestTargetSelector());
            _targetSelectors.AddLast(new MinHealthTargetSelector());
            _targetSelectors.AddLast(new MaxHealthTargetSelector());

            _currentSelectorNode = _targetSelectors.First;
        }

        public string GetSelectorDescription()
        {
            return CurrentSelector.Description;
        }

        public void ChangeSelector()
        {
            _currentSelectorNode = _currentSelectorNode.NextOrFirst();
            Target = CurrentSelector.GetTarget(_attackersInRange);
        }

        public void InitRange(float range)
        {
            _attackRange = range;
            _rangeCollider.radius = range;
        }

        private void OnAttackerDied(Attacker attacker)
        {
            _attackersInRange.Remove(attacker);

            if (attacker == Target)
                Target = CurrentSelector.GetTarget(_attackersInRange);

            attacker.Died -= OnAttackerDied;
            attacker.HealthChanged -= OnAttackerHealthChanged;
        }

        private void OnAttackerHealthChanged(Attacker changedHealth, int currentHealth, int maxHealth)
        {
            Target = CurrentSelector.AttackerHealthChanged(Target, changedHealth, _attackersInRange);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Attacker newAttacker))
            {
                _attackersInRange.Add(newAttacker);

                newAttacker.Died += OnAttackerDied;
                newAttacker.HealthChanged += OnAttackerHealthChanged;

                Target = CurrentSelector.NewAttackerInRange(Target, newAttacker);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Attacker attacker))
            {
                _attackersInRange.Remove(attacker);

                attacker.Died -= OnAttackerDied;
                attacker.HealthChanged -= OnAttackerHealthChanged;

                if (Target == attacker)
                    Target = CurrentSelector.GetTarget(_attackersInRange);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _attackRange);
        }
    }
}