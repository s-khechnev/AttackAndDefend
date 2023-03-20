using Attackers.Waves;
using Defender.HUD.Menu;
using UnityEngine.UI;

namespace Defender.HUD.Command
{
    public class StartNextWaveCommand : CommandBase
    {
        private readonly Spawner _spawner;

        public StartNextWaveCommand(GUIMenuBase panel, Spawner spawner) : base(panel)
        {
            _spawner = spawner;
        }

        public override bool CanExecute(Button button)
            => !_spawner.IsWavesEnded;

        public override void Execute(Button button)
        {
            _spawner.StartNextWave();
            Panel.Hide();
        }
    }
}