using UnityEngine;

namespace Helpers
{
    /// <summary>
    /// Component to maintain a constant width of the camera
    /// </summary>
    public class CameraConstantWidth : MonoBehaviour
    {
        [SerializeField] private Vector2 _defaultResolution;
        [SerializeField, Range(0f, 1f)] private float _widthOrHeight;

        private Camera _componentCamera;

        private float _initialSize;
        private float _targetAspect;

        private float _initialFov;
        private float _horizontalFov = 60f;

        private void Start()
        {
            _componentCamera = GetComponent<Camera>();
            _initialSize = _componentCamera.orthographicSize;

            _targetAspect = _defaultResolution.x / _defaultResolution.y;

            _initialFov = _componentCamera.fieldOfView;
            _horizontalFov = CalcVerticalFov(_initialFov, 1 / _targetAspect);
        }

        private void Update()
        {
            if (_componentCamera.orthographic)
            {
                var constantWidthSize = _initialSize * (_targetAspect / _componentCamera.aspect);
                _componentCamera.orthographicSize = Mathf.Lerp(constantWidthSize, _initialSize, _widthOrHeight);
            }
            else
            {
                var constantWidthFov = CalcVerticalFov(_horizontalFov, _componentCamera.aspect);
                _componentCamera.fieldOfView = Mathf.Lerp(constantWidthFov, _initialFov, _widthOrHeight);
            }
        }

        private float CalcVerticalFov(float hFovInDeg, float aspectRatio)
        {
            var hFovInRads = hFovInDeg * Mathf.Deg2Rad;

            var vFovInRads = 2 * Mathf.Atan(Mathf.Tan(hFovInRads / 2) / aspectRatio);

            return vFovInRads * Mathf.Rad2Deg;
        }
    }
}