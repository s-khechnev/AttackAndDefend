using UnityEngine;
using Zenject;

namespace Attackers.Movement
{
    /// <summary>
    /// The component responsible for the movement of the attacker
    /// </summary>
    [RequireComponent(typeof(Attacker))]
    public class AttackerMovement : MonoBehaviour
    {
        private AttackerData _attackerData;

        private Transform _currentPoint;
        private Route _route;

        /// <summary>
        /// Remaining distance to the castle
        /// </summary>
        public float DistanceToCastle { get; private set; }

        [Inject]
        private void Construct(Route route)
        {
            _route = route;

            DistanceToCastle = _route.DistanceToCastle;
            _currentPoint = _route.GetNextPoint(_currentPoint);
        }

        private void Awake()
        {
            _attackerData = GetComponent<Attacker>().AttackerData;
        }

        private void Start()
        {
            transform.position = _currentPoint.position;
        }

        private void Update()
        {
            if (_currentPoint == null) return;

            var stepDistance = _attackerData.Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _currentPoint.position, stepDistance);
            DistanceToCastle -= stepDistance;

            if (transform.position == _currentPoint.transform.position)
                _currentPoint = _route.GetNextPoint(_currentPoint);
        }
    }
}