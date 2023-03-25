using UnityEngine;

namespace Attackers.Movement
{
    public class Waypoints : MonoBehaviour
    {
        public float DistanceToCastle { get; private set; }

        private void Awake()
        {
            DistanceToCastle = GetDistanceToCastle();
        }

        public Transform GetNextPoint(Transform currentPoint)
        {
            if (currentPoint == null)
                return transform.GetChild(0);

            if (currentPoint.GetSiblingIndex() < transform.childCount - 1)
            {
                return transform.GetChild(currentPoint.GetSiblingIndex() + 1);
            }

            return null;
        }

        private float GetDistanceToCastle()
        {
            var result = 0f;
            for (var i = 1; i < transform.childCount; i++)
            {
                result += Vector3.Distance(transform.GetChild(i - 1).position, transform.GetChild(i).position);
            }

            return result;
        }
    }
}