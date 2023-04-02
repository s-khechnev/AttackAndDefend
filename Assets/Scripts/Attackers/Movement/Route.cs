using UnityEngine;

namespace Attackers.Movement
{
    public class Route : MonoBehaviour
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

            return currentPoint.GetSiblingIndex() < transform.childCount - 1
                ? transform.GetChild(currentPoint.GetSiblingIndex() + 1)
                : null;
        }

        private float GetDistanceToCastle()
        {
            var distanceToCastle = 0f;
            for (var i = 1; i < transform.childCount; i++)
            {
                distanceToCastle +=
                    Vector3.Distance(transform.GetChild(i - 1).position, transform.GetChild(i).position);
            }

            return distanceToCastle;
        }
    }
}