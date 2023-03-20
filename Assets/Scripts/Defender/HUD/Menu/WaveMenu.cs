using System;
using Attackers.Waves;
using Defender.HUD.Command;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Defender.HUD.Menu
{
    [Serializable]
    public class WaveMenu : GUIMenuBase
    {
        [SerializeField] private Button _startNextWaveButton;

        [Inject] private Spawner _spawner;

        public override void Init()
        {
            Instance = _startNextWaveButton.gameObject;

            AssociateButton(_startNextWaveButton, new StartNextWaveCommand(this, _spawner));

            _spawner.WaveEnded += OnWaveEnded;
            _spawner.AllWavesEnded += OnAllWavesEnded;
        }

        private void OnWaveEnded()
        {
            Show();
        }

        private void OnAllWavesEnded()
        {
            Debug.Log("Waves are over");
        }
    }
}