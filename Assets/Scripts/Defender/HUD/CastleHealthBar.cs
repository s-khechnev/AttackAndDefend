namespace Defender.HUD
{
    public class CastleHealthBar : Bar
    {
        private void OnEnable()
        {
            GameManager.Instance.CastleHealthChanged += OnCastleHealthChanged;
        }

        private void OnCastleHealthChanged(int value, int maxValue)
        {
            OnValueChanged(value, maxValue);
        }
    }
}