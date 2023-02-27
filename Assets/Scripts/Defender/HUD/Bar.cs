using UnityEngine;
using UnityEngine.UI;

namespace Defender.HUD
{
    [RequireComponent(typeof(Slider))]
    public abstract class Bar : MonoBehaviour
    {
        protected Slider Slider;

        private void Awake()
        {
            Slider = GetComponent<Slider>();
        }

        protected void OnValueChanged(int value, int maxValue)
        {
            Slider.value = (float)value / maxValue;
        }
    }
}