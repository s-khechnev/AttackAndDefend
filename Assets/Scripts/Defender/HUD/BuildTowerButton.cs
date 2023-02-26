using System;
using Data.Towers;
using UnityEngine;
using UnityEngine.UI;

namespace Defender.HUD
{
    [RequireComponent(typeof(Button))]
    public class BuildTowerButton : MonoBehaviour
    {
        public event Action<TowerData> BuildTowerClick;

        [SerializeField] private TowerData _towerData;

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
            BuildTowerClick?.Invoke(_towerData);
        }
    }
}