using UnityEngine;

namespace Attackers.Movement
{
    [RequireComponent(typeof(Attacker))]
    public class AttackerMovement : MonoBehaviour
    {
        private AttackerData _attackerData;

        private Waypoints _waypoints;
        private Transform _currentPoint;

        public float DistanceToCastle { get; private set; }

        private void Awake()
        {
            _attackerData = GetComponent<Attacker>().AttackerData;

            _waypoints = FindObjectOfType<Waypoints>();
            DistanceToCastle = _waypoints.DistanceToCastle;
            _currentPoint = _waypoints.GetNextPoint(_currentPoint);
            transform.position = _currentPoint.position;
        }

        private void Update()
        {
            if (_currentPoint != null)
            {
                var stepDistance = _attackerData.Speed * Time.deltaTime;

                transform.position =
                    Vector3.MoveTowards(transform.position, _currentPoint.position, stepDistance);

                DistanceToCastle -= stepDistance;

                if (transform.position == _currentPoint.transform.position)
                    _currentPoint = _waypoints.GetNextPoint(_currentPoint);
            }
        }
    }
}