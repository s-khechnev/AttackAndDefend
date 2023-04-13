using UnityEngine;

namespace Attackers.Movement
{
    public class Route : MonoBehaviour
    {
        /// <summary>
        /// Distance of route to castle
        /// </summary>
        public float DistanceToCastle { get; private set; }

        private void Awake()
        {
            DistanceToCastle = GetDistanceToCastle();
        }

        /// <summary>
        /// Get the next route point
        /// </summary>
        /// <param name="currentPoint">current point</param>
        /// <returns>Next point relative to the current one</returns>
        public Transform GetNextPoint(Transform currentPoint)
        {
            if (currentPoint == null)
                return transform.GetChild(0);

            return currentPoint.GetSiblingIndex() < transform.childCount - 1
                ? transform.GetChild(currentPoint.GetSiblingIndex() + 1)
                : null;
        }

        /// <summary>
        /// Get the distance of route to castle 
        /// </summary>
        /// <returns>Distance of route to castle</returns>
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