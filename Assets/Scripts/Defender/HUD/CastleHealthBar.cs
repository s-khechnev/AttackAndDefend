using Models;
using Zenject;

namespace Defender.HUD
{
    public class CastleHealthBar : Bar
    {
        [Inject] private Castle _castle;

        private void Start()
        {
            _castle.HealthChanged += OnHealthChanged;
        }

        private void OnHealthChanged(int value, int maxValue)
        {
            OnValueChanged(value, maxValue);
        }
    }
}