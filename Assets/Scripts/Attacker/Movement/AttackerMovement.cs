using UnityEngine;

namespace Attacker.Movement
{
    public class AttackerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Waypoints _waypoints;
        private Transform _currentPoint;

        private void Start()
        {
            _waypoints = FindObjectOfType<Waypoints>();
            _currentPoint = _waypoints.GetNextPoint(_currentPoint);

            transform.position = _currentPoint.position;
        }

        private void Update()
        {
            if (_currentPoint != null)
            {
                transform.position =
                    Vector3.MoveTowards(transform.position, _currentPoint.position, _speed * Time.deltaTime);

                if (transform.position == _currentPoint.transform.position)
                    _currentPoint = _waypoints.GetNextPoint(_currentPoint);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Destroy(gameObject);
        }
    }
}