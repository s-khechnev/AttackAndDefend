using Models;
using Zenject;

namespace Defender.HUD.Bars
{
    public class CastleHealthBar : Bar
    {
        private Castle _castle;

        [Inject]
        private void Construct(Castle castle)
        {
            _castle = castle;
        }

        private void Start()
        {
            _castle.HealthChanged += OnHealthChanged;
        }

        private void OnHealthChanged(int value, int maxValue)
        {
            ChangeValue(value, maxValue);
        }
    }
}