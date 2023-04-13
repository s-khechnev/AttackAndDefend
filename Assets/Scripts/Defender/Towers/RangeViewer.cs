using UnityEngine;

namespace Defender.Towers
{
    /// <summary>
    /// Component that draws a circle using a LineRenderer around a gameObject 
    /// </summary>
    public class RangeViewer
    {
        private const int NumSegments = 300;
        private const float OffsetY = .1f;

        private readonly LineRenderer _lineRenderer;

        public RangeViewer(LineRenderer lineRenderer, Material circleMaterial)
        {
            _lineRenderer = lineRenderer;

            InitLineRenderer(circleMaterial);
        }

        private void InitLineRenderer(Material circleMaterial)
        {
            _lineRenderer.material = circleMaterial;
            _lineRenderer.startWidth = 0.1f;
            _lineRenderer.endWidth = 0.1f;
            _lineRenderer.positionCount = NumSegments + 1;
            _lineRenderer.useWorldSpace = false;
            _lineRenderer.loop = true;
        }

        /// <summary>
        /// Draw the circle
        /// </summary>
        /// <param name="radius">radius of the circle</param>
        public void DrawCircle(float radius)
        {
            const float deltaTheta = (float)(2.0 * Mathf.PI) / NumSegments;
            var theta = 0f;

            for (var i = 0; i < NumSegments + 1; i++)
            {
                var x = radius * Mathf.Cos(theta);
                var z = radius * Mathf.Sin(theta);
                var pos = new Vector3(x, OffsetY, z);
                _lineRenderer.SetPosition(i, pos);
                theta += deltaTheta;
            }
        }

        public void Hide()
        {
            _lineRenderer.enabled = false;
        }

        public void Show()
        {
            _lineRenderer.enabled = true;
        }
    }
}