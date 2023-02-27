using UnityEngine;
using UnityEngine.UI;

namespace Defender.HUD
{
    [RequireComponent(typeof(Slider))]
    public abstract class Bar : MonoBehaviour
    {
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        protected void OnValueChanged(int value, int maxValue)
        {
            _slider.value = (float)value / maxValue;
        }
    }
}