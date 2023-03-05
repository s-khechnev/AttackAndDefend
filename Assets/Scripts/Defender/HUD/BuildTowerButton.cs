using System;
using Defender.Towers;
using UnityEngine;
using UnityEngine.UI;

namespace Defender.HUD
{
    [RequireComponent(typeof(Button))]
    public class BuildTowerButton : MonoBehaviour
    {
        public event Action<Tower> BuildTowerTapped;

        [SerializeField] private Tower _towerToBuild;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            BuildTowerTapped?.Invoke(_towerToBuild);
        }
    }
}