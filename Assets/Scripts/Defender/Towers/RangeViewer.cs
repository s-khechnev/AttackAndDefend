using UnityEngine;

namespace Defender.Towers
{
    [RequireComponent(typeof(LineRenderer))]
    public class RangeViewer : MonoBehaviour
    {
        [SerializeField] private Material _circleMaterial;

        private const int NumSegments = 300;
        private LineRenderer _lineRenderer;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            InitLineRenderer();
        }

        private void InitLineRenderer()
        {
            _lineRenderer.material = _circleMaterial;
            _lineRenderer.startWidth = 0.1f;
            _lineRenderer.endWidth = 0.1f;
            _lineRenderer.positionCount = NumSegments + 1;
            _lineRenderer.useWorldSpace = false;
        }

        public void DrawCircle(float radius)
        {
            const float deltaTheta = (float)(2.0 * Mathf.PI) / NumSegments;
            var theta = 0f;

            for (var i = 0; i < NumSegments + 1; i++)
            {
                var x = radius * Mathf.Cos(theta);
                var z = radius * Mathf.Sin(theta);
                var pos = new Vector3(x, 0, z);
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