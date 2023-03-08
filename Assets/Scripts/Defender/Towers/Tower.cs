using System.Collections.Generic;
using Attackers;
using Data.Towers;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Defender.Towers
{
    [RequireComponent(typeof(SphereCollider))]
    public abstract class Tower : MonoBehaviour
    {
        [Inject] protected TowerFactory TowerFactory;
        [Inject] protected WarFactory WarFactory;

        [SerializeField] protected Transform _launchPoint;
        [SerializeField] private TowerData _towerData;
        [SerializeField] private Transform _pivot;

        protected Attacker Target;

        private SphereCollider _rangeCollider;
        private Queue<Attacker> _attackersQueue;
        private float _elapsedTimeFromShoot;

        private float AttackRange => _towerData.AttackRange;
        private float Cooldown => _towerData.Cooldown;
        public TowerData TowerData => _towerData;

        protected abstract void Shoot();

        private void Awake()
        {
            _rangeCollider = GetComponent<SphereCollider>();
            _rangeCollider.radius = AttackRange;

            _attackersQueue = new();

            _elapsedTimeFromShoot = Cooldown;
        }

        private void Update()
        {
            if (_attackersQueue.Count != 0 && (Target == null || Target.IsDestroyed()))
            {
                Target = _attackersQueue.Dequeue();
            }

            _elapsedTimeFromShoot += Time.deltaTime;
            if (Target != null)
            {
                _pivot.LookAt(Target.transform);
                
                if (_elapsedTimeFromShoot >= Cooldown)
                {
                    Shoot();
                    _elapsedTimeFromShoot = 0;
                }
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
            Gizmos.DrawWireSphere(transform.position, AttackRange);
        }
    }
}