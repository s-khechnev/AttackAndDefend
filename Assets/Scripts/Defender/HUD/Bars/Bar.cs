using UnityEngine;
using UnityEngine.UI;

namespace Defender.HUD.Bars
{
    [RequireComponent(typeof(Slider))]
    public abstract class Bar : MonoBehaviour
    {
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        /// <summary>
        /// Change the filling of the bar
        /// </summary>
        /// <param name="value">current value</param>
        /// <param name="maxValue">max value</param>
        protected void ChangeValue(int value, int maxValue)
        {
            _slider.value = (float)value / maxValue;
        }
    }
}