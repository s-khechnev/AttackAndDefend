using Data.Attackers;
using UnityEngine;

namespace Attackers.Movement
{
    [RequireComponent(typeof(Attacker))]
    public class AttackerMovement : MonoBehaviour
    {
        private AttackerData _attackerData;

        private Waypoints _waypoints;
        private Transform _currentPoint;

        private void Awake()
        {
            _attackerData = GetComponent<Attacker>().AttackerData;

            _waypoints = FindObjectOfType<Waypoints>();
            _currentPoint = _waypoints.GetNextPoint(_currentPoint);
            transform.position = _currentPoint.position;
        }

        private void Update()
        {
            if (_currentPoint != null)
            {
                transform.position =
                    Vector3.MoveTowards(transform.position, _currentPoint.position,
                        _attackerData.Speed * Time.deltaTime);

                if (transform.position == _currentPoint.transform.position)
                    _currentPoint = _waypoints.GetNextPoint(_currentPoint);
            }
        }
    }
}